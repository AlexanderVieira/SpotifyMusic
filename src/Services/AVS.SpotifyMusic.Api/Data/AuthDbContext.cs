using AVS.SpotifyMusic.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.JwtSigningCredentials;
using NetDevPack.Security.JwtSigningCredentials.Store.EntityFrameworkCore;

namespace AVS.SpotifyMusic.Api.Data
{
    public class AuthDbContext : IdentityDbContext, ISecurityKeyContext
    {        
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }

        //DbSet<KeyMaterial> ISecurityKeyContext.SecurityKeys { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}