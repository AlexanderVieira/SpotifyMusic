using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Core.Data
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot, new()
    {
        private readonly SpotifyMusicContext _context;
        private bool _disposedValue;

        public IUnitOfWork UnitOfWork => _context;
        public DbSet<TEntity> Query { get; set; }

        public BaseRepository(SpotifyMusicContext context)
        {
            _context = context;
            Query = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(Expression<Func<TEntity, bool>> predicado)
        {
            return await Query.AsNoTracking().Where(predicado).ToListAsync();
        }

        public async Task<TEntity> BuscarPorCriterio(Expression<Func<TEntity, bool>> predicado)
        {
            return await Query.FirstOrDefaultAsync(predicado);
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await Query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await Query.FindAsync(id);
        }

        public async Task Salvar(TEntity entity)
        {
            await Query.AddAsync(entity);
        }

        public async Task Atualizar(TEntity entity)
        {
            Query.Update(entity);
            await Task.CompletedTask;
        }

        public async Task Remover(Guid id)
        {
            Query.Remove(new TEntity { Id = id });
            await Task.CompletedTask;
        }        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Query = null;
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }

        ~BaseRepository()
        {
            Dispose(false);
        }
    }
}
