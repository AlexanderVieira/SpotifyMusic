using AVS.SpotifyMusic.Domain.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;

namespace AVS.SpotifyMusic.Domain.Contas.Services
{
    public class BaseService<TEntity> : IService<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public Task Atualizar(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> BuscarPorCriterio(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicado)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicado)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Salvar(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
