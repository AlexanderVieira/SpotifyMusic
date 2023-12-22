using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Services;

namespace AVS.SpotifyMusic.Domain.Contas.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {            
            _usuarioRepository = usuarioRepository;
        }       

        public async void Ativar(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);            
            usuario.Ativar();
            await _usuarioRepository.Atualizar(usuario);
            var result = await _usuarioRepository.UnitOfWork.Commit();
        }

        public async void Inativar(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            usuario.Inativar();
            await _usuarioRepository.Atualizar(usuario);
            var result = await _usuarioRepository.UnitOfWork.Commit();
        }
    }
}
