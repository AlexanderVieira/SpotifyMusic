using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Infra.Data.Context;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;

namespace AVS.SpotifyMusic.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly SpotifyMusicContext _context;
        public UsuarioRepository(SpotifyMusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarPorCriterioDetalhado(Expression<Func<Usuario, bool>> expression)
        {
            var result = await _context.Usuarios
                .Include(u => u.Cartoes)
                .ThenInclude(c => c.Pagamento)
                .ThenInclude(p => p.Transacao)
                .Include(u => u.Assinaturas)
                .ThenInclude(a => a.Plano)
                .Include(u => u.Playlists)
                .ThenInclude(p => p.Musicas)
                .FirstOrDefaultAsync(expression);
            return result;
        }

        public async Task<bool> Existe(Expression<Func<Usuario, bool>> expression)
        {
            var result = await _context.Usuarios.Where(expression).AnyAsync();
            return result;
        }

        public async Task<IEnumerable<UsuarioConsultaAnonima>> BuscarTodosConsultaProjetada()
        {
            // Consulta projetada
            var result = await Query.Select(u =>
                                         new UsuarioConsultaAnonima
                                         {
                                             Id = u.Id,
                                             Nome = u.Nome,
                                             Email = u.Email.Address,
                                             Foto = u.Foto

                                         }).ToListAsync();

            return result;

        }

        public async Task<IEnumerable<UsuarioConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Usuario, bool>> expression)
        {
            // Consulta projetada
            var result = await Query.Where(expression)
                                      .Select(u =>
                                         new UsuarioConsultaAnonima 
                                         { 
                                             Id = u.Id, 
                                             Nome = u.Nome, 
                                             Email = u.Email.Address, 
                                             Foto = u.Foto
                                             
                                         }).ToListAsync();

            return result;


        }
    }
    
}
