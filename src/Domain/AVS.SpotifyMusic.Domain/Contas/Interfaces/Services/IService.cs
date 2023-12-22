using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Contas.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : Entity, IAggregateRoot
    {
        Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(Expression<Func<TEntity, bool>> predicado);
        Task<TEntity> BuscarPorCriterio(Expression<Func<TEntity, bool>> predicado);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);
        Task Salvar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
    }
}
