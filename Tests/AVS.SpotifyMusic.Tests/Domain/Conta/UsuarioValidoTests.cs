using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Enums;
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
        public void Usuario_CriarInstancia_DeveEstarValido()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            var result = usuario?.EhValido();

            //Assert
            Assert.True(result);
            Assert.Equal(0, usuario?.ValidationResult?.Errors.Count);
        }

        [Fact(DisplayName = "Usuario Criar Playlist")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_NovaPlaylist_DeveTerMaisDeUma()
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
        public void Usuario_AtualizarPlaylists_DeveTerTamanhoMaiorOuIgualUm()
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

        public void Usuario_RemoverPlaylist_DeveTerTamanhoIgualAoLenghtMenosUm()
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
        public void Usuario_RemoverPlaylists_DeveTerTamanhoIgualZero()
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
        public void Usuario_Ativar_DeveTerFlagIgualVerdadeiro()
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
        public void Usuario_Inativar_DeveTerFlagIgualFalso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            usuario?.Inativar();

            //Assert            
            Assert.False(usuario?.Ativo);
        }

        [Fact(DisplayName = "Usuario Criar Assinatura")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_CriarAssinatura_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            var plano = new Plano("My Plano", "Plano gratuito", 0, TipoPlano.Basico);

            //Act
            usuario?.CriarAssinatura(plano);

            //Assert            
            Assert.True(usuario?.Assinaturas.Any(x => x.Ativo == true)); 
        }

        [Fact(DisplayName = "Usuario Atualizar Plano")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_AtualizarPlano_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido(); 
            usuario?.CriarAssinatura(new Plano("My Plano Basico",
                                               "Plano que permite ouvir músicas favoritas",
                                               0,
                                               TipoPlano.Basico));

            //Act
            usuario?.AtualizarPlano(new Plano("My Plano Premium", 
                                              "Plano que permite download das músicas favoritas", 
                                              14.99M, 
                                              TipoPlano.Premium));

            //Assert            
            Assert.True(usuario?.Assinaturas.Any(x => x.Plano.TipoPlano == TipoPlano.Premium));
        }

        [Fact(DisplayName = "Usuario Adicionar Cartão")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_AdicionarCartao_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            usuario?.CriarAssinatura(new Plano("My Plano Basico",
                                               "Plano que permite ouvir músicas favoritas",
                                               0,
                                               TipoPlano.Basico));

            //Act
            usuario?.AdicionarCartao(_fixture.CriarCartaoValido());

            //Assert            
            Assert.True(usuario?.Cartoes.Any());
        }
    }
}
