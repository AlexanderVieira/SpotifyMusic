using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Application.Contas.Services
{
	public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {            
            _usuarioRepository = usuarioRepository;
        }       

        public async Task<bool> Ativar(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
			if (usuario == null) return false;
			usuario.Ativar();
            await _usuarioRepository.Atualizar(usuario);
            var result = await _usuarioRepository.UnitOfWork.Commit();
            return result;
        }

        public async Task<IEnumerable<UsuarioConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Usuario, bool>> expression)
        {
            var usuarios = await _usuarioRepository.BuscarPorCriterioConsultaProjetada(expression);
            return usuarios;
        }

        public async Task<Usuario> BuscarPorCriterioDetalhado(Expression<Func<Usuario, bool>> expression)
        {
            var usuario = await _usuarioRepository.BuscarPorCriterioDetalhado(expression);
            return usuario;
        }

        public async Task<IEnumerable<UsuarioConsultaAnonima>> BuscarTodosConsultaProjetada()
        {
            var result = await _usuarioRepository.BuscarTodosConsultaProjetada();           
            return result;
        }

        public async Task<bool> Existe(Expression<Func<Usuario, bool>> expression)
        {
            var result = await _usuarioRepository.Existe(expression);
            return result;
        }

        public async Task<bool> Inativar(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null) return false;
            usuario.Inativar();
            await _usuarioRepository.Atualizar(usuario);
            var result = await _usuarioRepository.UnitOfWork.Commit();
            return result;
        }

    }
}
