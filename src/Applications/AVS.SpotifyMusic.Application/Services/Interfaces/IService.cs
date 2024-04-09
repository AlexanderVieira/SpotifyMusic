using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Application.Contas.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : Entity, IAggregateRoot
    {
        Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(Expression<Func<TEntity, bool>> predicado);
        Task<TEntity> BuscarPorCriterio(Expression<Func<TEntity, bool>> predicado);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);
        Task<bool> Salvar(TEntity entity);
        Task<bool> Atualizar(TEntity entity);
        Task<bool> Remover(Guid id);
    }
}
