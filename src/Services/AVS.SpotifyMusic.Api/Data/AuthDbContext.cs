using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.SpotifyMusic.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace AVS.SpotifyMusic.Api.Data
{
    public class AuthDbContext : IdentityDbContext, ISecurityKeyContext
    {        
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<KeyMaterial> ISecurityKeyContext.SecurityKeys { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}