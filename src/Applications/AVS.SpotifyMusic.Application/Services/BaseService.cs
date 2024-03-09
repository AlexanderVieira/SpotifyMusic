using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Application.Contas.Services
{
	public class BaseService<TEntity> : IService<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Atualizar(TEntity entity)
        {
            await _repository.Atualizar(entity);
            var result = await _repository.UnitOfWork.Commit();
            return result;
        }

        public async Task<TEntity> BuscarPorCriterio(Expression<Func<TEntity, bool>> predicado)
        {
            var result = await _repository.BuscarPorCriterio(predicado); 
            return result;
        }

        public async Task<IEnumerable<TEntity>> BuscarTodosPorCriterio(Expression<Func<TEntity, bool>> predicado)
        {
			var result = await _repository.BuscarTodosPorCriterio(predicado);
			return result;
		}

        public async Task<TEntity> ObterPorId(Guid id)
        {
            var result = await _repository.ObterPorId(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            var result = await _repository.ObterTodos();
            return result;

		}

        public async Task<bool> Remover(Guid id)
        {
            var usuario = await _repository.ObterPorId(id);
            if(usuario == null) return false;
            await _repository.Remover(id);
            var result = await _repository.UnitOfWork.Commit();
            return result;
        }

        public async Task<bool> Salvar(TEntity entity)
        {
            await _repository.Salvar(entity);
            var result = await _repository.UnitOfWork.Commit();
            return result;
        }

    }
}
