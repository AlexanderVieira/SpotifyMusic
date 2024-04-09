using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(Expression<Func<TEntity, bool>> predicado);
        Task<TEntity> BuscarPorCriterio(Expression<Func<TEntity, bool>> predicado);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);
        Task Salvar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        void DetachLocal(Func<TEntity, bool> predicado);
        void ModifyLocal(Func<TEntity, bool> predicado);

    }
}
