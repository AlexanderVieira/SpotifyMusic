using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Infra.Data.Context;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AVS.SpotifyMusic.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly SpotifyMusicContext _context;
        public UsuarioRepository(SpotifyMusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Existe(Expression<Func<Usuario, bool>> expression)
        {
            var result = await _context.Usuarios.Where(expression).AnyAsync();
            return result;
        }
    }
    
}
