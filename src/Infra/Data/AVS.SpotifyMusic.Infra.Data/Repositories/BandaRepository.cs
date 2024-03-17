using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Infra.Data.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Interfaces.Repositories;

namespace AVS.SpotifyMusic.Infra.Data.Repositories
{
    public class BandaRepository : BaseRepository<Banda>, IBandaRepository
    {
        private readonly SpotifyMusicContext _context;
        public BandaRepository(SpotifyMusicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Banda> BuscarPorCriterioDetalhado(Expression<Func<Banda, bool>> expression)
        {
            var result = await Query
                .Include(u => u.Albuns)
                .ThenInclude(c => c.Musicas)                
                .FirstOrDefaultAsync(expression);
            return result;
        }

        public async Task<bool> Existe(Expression<Func<Banda, bool>> expression)
        {
            var result = await Query.Where(expression).AnyAsync();
            return result;
        }

        public async Task<IEnumerable<BandaConsultaAnonima>> BuscarTodosConsultaProjetada()
        {
            // Consulta projetada
            var result = await Query.Select(u =>
                                         new BandaConsultaAnonima
                                         {
                                             Id = u.Id,
                                             Nome = u.Nome,
                                             Descricao = u.Descricao,
                                             Foto = u.Foto

                                         }).ToListAsync();

            return result;

        }

        public async Task<IEnumerable<BandaConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Banda, bool>> expression)
        {
            // Consulta projetada
            var result = await Query.Where(expression)
                                      .Select(u =>
                                         new BandaConsultaAnonima
                                         { 
                                             Id = u.Id, 
                                             Nome = u.Nome, 
                                             Descricao = u.Descricao, 
                                             Foto = u.Foto
                                             
                                         }).ToListAsync();

            return result;


        }

        public async Task<bool> CriarAlbum(Banda banda) 
        {          
                                   
            _context.Entry(banda).State = EntityState.Modified;

            foreach (var album in banda.Albuns.ToList()) 
            {                
                _context.Entry(album).State = !_context.Albuns.Any(x => x.Id == album.Id) ? EntityState.Added : EntityState.Modified;

                foreach (var musica in album.Musicas.ToList())
                {
                    _context.Entry(musica).State = !_context.Musicas.Any(x => x.Id == musica.Id) ? EntityState.Added : EntityState.Modified;
                }
            }

            return await UnitOfWork.Commit();
        }
    }
    
}
