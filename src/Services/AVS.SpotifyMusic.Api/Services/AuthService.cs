using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AVS.SpotifyMusic.Api.Data;
using AVS.SpotifyMusic.Api.Models;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.AspNetUser.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.JwtSigningCredentials.Interfaces;

namespace AVS.SpotifyMusic.Api.Services
{
    public class AuthService
    {
        public readonly SignInManager<IdentityUser> SignInManager;
        public readonly UserManager<IdentityUser> UserManager;        
        private readonly AppTokenSettings _appTokenSettings;
        private readonly AuthDbContext _context;
        private readonly IJsonWebKeySetService _jwksService;
        private readonly IAspNetUser _aspNetUser;

        public AuthService(SignInManager<IdentityUser> signInManager, 
                                     UserManager<IdentityUser> userManager,
                                     IOptions<AppTokenSettings> appTokenSettings,
                                     AuthDbContext context,
                                     IJsonWebKeySetService jwksService,
                                     IAspNetUser aspNetUser)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            _appTokenSettings = appTokenSettings.Value;
            _context = context;
            _jwksService = jwksService;
            _aspNetUser = aspNetUser;
        }

        public async Task<UserResponseLogin> GenerateJwt(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            var claims = await UserManager.GetClaimsAsync(user);
            var identityClaims = await GetClaimUser(claims, user);
            var encodedToken = EncodeToken(identityClaims);
            var refreshToken = await GenerateRefreshToken(email);
            return GetResponseToken(encodedToken, user, claims, refreshToken);
        }

        private UserResponseLogin GetResponseToken(string encodedToken, IdentityUser user, IList<Claim> claims, RefreshToken refreshToken)
        {
            return new UserResponseLogin
            {
                AccessToken = encodedToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromMinutes(5).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private async Task<RefreshToken> GenerateRefreshToken(string email)
        {
            var refreshToken = new RefreshToken
            {
                Username = email,
                ExpirationDate = DateTime.UtcNow.AddMinutes(_appTokenSettings.RefreshTokenExpiration)
            };
            _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(u => u.Username == email));
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var currentIssuer = $"{_aspNetUser.GetHttpContext().Request.Scheme}://{_aspNetUser.GetHttpContext().Request.Host}";
            var key = _jwksService.GetCurrent();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor 
            { 
                Issuer = currentIssuer, 
                Subject = identityClaims, 
                Expires = DateTime.UtcNow.AddMinutes(5), 
                SigningCredentials = key 
            });
            return tokenHandler.WriteToken(token);
        }

        private async Task<ClaimsIdentity> GetClaimUser(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            foreach (var item in userRoles)
            {
                claims.Add(new Claim("role", item));
            }
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date)
         => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public async Task<RefreshToken> GetRefreshToken(Guid refreshToken)
        {
            var token = await _context.RefreshTokens.AsNoTracking()
                                      .FirstOrDefaultAsync(u => u.Token == refreshToken);

            return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now ? token : null;
        }
    }
}