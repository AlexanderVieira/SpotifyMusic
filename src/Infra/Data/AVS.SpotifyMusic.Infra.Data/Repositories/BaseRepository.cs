using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Core.Data
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot, new()
    {
        private readonly SpotifyMusicContext _context;
        

        public IUnitOfWork UnitOfWork => _context;
        public DbSet<TEntity> Query { get; set; }

        public BaseRepository(SpotifyMusicContext context)
        {
            _context = context;
            Query = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(Expression<Func<TEntity, bool>> predicado)
        {
            return await Query.Where(predicado).ToListAsync();
        }

        public async Task<TEntity> BuscarPorCriterio(Expression<Func<TEntity, bool>> predicado)
        {
            var result = await Query.Where(predicado).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await Query.AsQueryable().ToListAsync();
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
        
        public void DetachLocal(Func<TEntity, bool> predicado)
        {
            var local = _context.Set<TEntity>().Local.Where(predicado).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public void ModifyLocal(Func<TEntity, bool> predicado)
        {
            var local = _context.Set<TEntity>().Local.Where(predicado).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Modified;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
