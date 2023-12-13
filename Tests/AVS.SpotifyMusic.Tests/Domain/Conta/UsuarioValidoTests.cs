using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Tests.Fixtures;

namespace AVS.SpotifyMusic.Tests.Domain.Conta
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioValidoTests
    {
        private readonly UsuarioFixtureTests _fixture;

        public UsuarioValidoTests(UsuarioFixtureTests fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Novo Usuario valido")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Criar_NovoUsuario_DeveEstarValido()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            var result = usuario?.EhValido();

            //Assert
            Assert.True(result.HasValue);
            Assert.Equal(0, usuario?.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Usuario Criar Playlist")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Criar_NovaPlaylist_DeveTerMaisDeUma()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            var playlist = new Playlist("Titulo", "Descricao", "foto", true, usuario);

            //Act
            usuario.AdicionarPlaylist(playlist);

            //Assert
            Assert.True(usuario.Playlists.Any());
           
        }

        [Fact(DisplayName = "Usuario Atualizar Playlist")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Atualizar_Playlists_DeveTerTamanhoMaiorOuIgualUm()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();            
            var playlists = new List<Playlist> 
            { 
                new Playlist("Titulo", "Descricao", "foto", true, usuario),
                new Playlist("Titulo-1", "Descricao-1", "foto-1", true, usuario)
            };            

            //Act
            usuario.AtualizarPlaylist(playlists);

            //Assert
            Assert.True(usuario.Playlists.Any());
        }

        [Fact(DisplayName = "Usuario Remover Playlist")]
        [Trait("Categoria", "Usuario Bogus Testes")]

        public void Remover_UmaPlaylist_DeveTerTamanhoIgualAoLenghtMenosUm()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            var playlists = new List<Playlist>
            {
                new Playlist("Titulo", "Descricao", "foto", true, usuario),
                new Playlist("Titulo-1", "Descricao-1", "foto-1", true, usuario)
            };
            usuario.AtualizarPlaylist(playlists);
            var countExpected = playlists.Count - 1;
            //Act
            usuario.RemoverPlaylist(playlists[1]);

            //Assert            
            Assert.Equal(countExpected, usuario.Playlists.Count);
        }

        [Fact(DisplayName = "Usuario Remover Todas Playlists")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Remover_Playlists_DeveTerTamanhoIgualZero()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            var playlists = new List<Playlist>
            {
                new Playlist("Titulo", "Descricao", "foto", true, usuario),
                new Playlist("Titulo-1", "Descricao-1", "foto-1", true, usuario)
            };
            usuario.AtualizarPlaylist(playlists);

            //Act
            usuario.RemoverPlaylists();

            //Assert            
            Assert.True(usuario.Playlists.Count == 0);
        }

        [Fact(DisplayName = "Usuario Ativo")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Ativar_NovoUsuario_DeveTerFlagIgualVerdadeiro()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            usuario?.Ativar();

            //Assert            
            Assert.True(usuario?.Ativo);
        }

        [Fact(DisplayName = "Usuario Inativo")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Inativar_Usuario_DeveTerFlagIgualFalso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            usuario?.Inativar();

            //Assert            
            Assert.False(usuario?.Ativo);
        }
    }
}
