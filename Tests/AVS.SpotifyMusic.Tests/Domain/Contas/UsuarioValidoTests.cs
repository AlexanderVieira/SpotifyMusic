using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Enums;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Enums;
using AVS.SpotifyMusic.Tests.Builders;
using AVS.SpotifyMusic.Tests.Fixtures;
using FluentAssertions;

namespace AVS.SpotifyMusic.Tests.Domain.Contas
{
    [Collection(nameof(UsuarioCollection))]    
    public class UsuarioValidoTests
    {
        private readonly UsuarioFixtureTests _fixture;        

        public UsuarioValidoTests(UsuarioFixtureTests fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Novo Usuario Válido com sucesso")]
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

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Fact(DisplayName = "Novo Usuario Foto Limite comprimento URL com sucesso")]        
        public void Usuario_ValidarFotoLimiteComprimentoUrl_ComSucesso()
        {
            //Arrange                        
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.True(result);
            result.Should().Be(usuario.Foto.Length > 0 && usuario.Foto.Length <= 8178);

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Fact(DisplayName = "Novo Usuario Nome com sucesso")]
        public void Usuario_ValidarNome_ComSucesso()
        {
            //Arrange                        
            var usuario = _fixture.CriarUsuarioValido();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.True(result);
            Assert.NotEmpty(usuario.Nome);
            Assert.NotNull(usuario.Nome);
            result.Should().NotBe(usuario.Nome == null || usuario.Nome == string.Empty);

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Validar Senha com sucesso")]
        [InlineData("Th1sIsapassword!", true)]
        [InlineData("thisIsapassword123!", true)]
        [InlineData("Abc$123456", true)]
        [InlineData("Th1s!", false)]
        [InlineData("thisIsAPassword", false)]
        [InlineData("thisisapassword#", false)]
        [InlineData("THISISAPASSWORD123!", false)]
        [InlineData("", false)]
        public void Usuario_ValidarSenha_ComSucesso(string senhaInvalida, bool senhaEsperada)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComSenha(senhaInvalida).Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.Equal(senhaEsperada, result);            

        }

        [Fact(DisplayName = "Usuario Criar Playlist com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_CriarPlaylist_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();            

            //Act
            usuario.CriarPlaylist("Titulo", "Descricao", true, "foto");

            //Assert
            Assert.True(usuario?.Playlists.Any(x => x.Titulo.Contains("Titulo")));
           
        }

        [Fact(DisplayName = "Usuario Atualizar Playlist com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_AtualizarPlaylists_DeveTerTamanhoMaiorOuIgualUm()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();            
            var playlists = new List<Playlist> 
            { 
                new Playlist("Titulo", "Descricao", true, usuario, "foto"),
                new Playlist("Titulo-1", "Descricao-1", true, usuario, "foto-1")
            };            

            //Act
            usuario.AtualizarPlaylist(playlists);

            //Assert
            Assert.True(usuario.Playlists.Any());
        }

        [Fact(DisplayName = "Usuario Remover Playlist com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]

        public void Usuario_RemoverPlaylist_DeveTerTamanhoIgualAoLenghtMenosUm()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            var playlists = new List<Playlist>
            {
                new Playlist("Titulo", "Descricao", true, usuario, "foto"),
                new Playlist("Titulo-1", "Descricao-1", true, usuario, "foto-1")
            };
            usuario.AtualizarPlaylist(playlists);
            var countExpected = playlists.Count - 1;
            //Act
            usuario.RemoverPlaylist(playlists[1]);

            //Assert            
            Assert.Equal(countExpected, usuario.Playlists.Count);
        }

        [Fact(DisplayName = "Usuario Remover Todas Playlists com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_RemoverPlaylists_DeveTerTamanhoIgualZero()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            var playlists = new List<Playlist>
            {
                new Playlist("Titulo", "Descricao", true, usuario, "foto"),
                new Playlist("Titulo-1", "Descricao-1", true, usuario, "foto-1")
            };
            usuario.AtualizarPlaylist(playlists);

            //Act
            usuario.LimparPlaylists();

            //Assert            
            Assert.True(usuario.Playlists.Count == 0);
        }

        [Fact(DisplayName = "Usuario Ativo com sucesso")]
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

        [Fact(DisplayName = "Usuario Inativo com sucesso")]
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

        [Fact(DisplayName = "Usuario Criar Assinatura com sucesso")]
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

        [Fact(DisplayName = "Usuario Desativar Assinatura Ativa com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_DesativarAssinaturaAtiva_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();
            usuario?.CriarAssinatura(new Plano("My Plano Basico",
                                               "Plano que permite ouvir músicas favoritas",
                                               0,
                                               TipoPlano.Basico));

            //Act
            usuario?.DesativarAssinaturaAtiva();

            //Assert            
            Assert.False(usuario?.Assinaturas.Any(x => x.Ativo));
        }

        [Fact(DisplayName = "Usuario Assinar Plano com sucesso com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_AssinarPlano_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();            

            var pagamento = new Pagamento(590.00M,
                StatusPagamento.Pago,
                new Cartao("5464-1733-1700-6552", "Patrícia I Heloisa Viana", "05/26", "198", true, 5000.00M),
                new Transacao(14.99M, "Spotify Music", StatusTransacao.Pago));           

            //Act
            usuario?.AssinarPlano(new Plano("My Plano Premium",
                                              "Plano que permite download das músicas favoritas",
                                              14.99M,
                                              TipoPlano.Premium), pagamento);

            //Assert            
            Assert.True(usuario?.Assinaturas.Any(x => x.Plano.TipoPlano == TipoPlano.Premium));
            Assert.True(usuario?.Assinaturas.Any(x => x.Ativo));
            Assert.True(pagamento?.Cartao.Transacoes.Any(x => x.Situacao == StatusTransacao.Pago));
            
        }


        [Fact(DisplayName = "Usuario Atualizar Plano com sucesso")]
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

        [Fact(DisplayName = "Usuario Criar Conta com sucesso")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_CriarConta_ComSucesso()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioValido();            

            var pagamento = new Pagamento(590.00M,
                StatusPagamento.Pago,
                new Cartao("5464-1733-1700-6552", "Patrícia I Heloisa Viana", "05/26", "198", true, 5000.00M),
                new Transacao(14.99M, "Spotify Music", StatusTransacao.Pago));

            //Act
            usuario?.CriarConta(new Plano("My Plano Premium",
                                              "Plano que permite download das músicas favoritas",
                                              14.99M,
                                              TipoPlano.Premium), pagamento);

            //Assert            
            Assert.True(usuario?.Assinaturas.Any(x => x.Plano.TipoPlano == TipoPlano.Premium));
            Assert.True(usuario?.Assinaturas.Any(x => x.Ativo));
            Assert.True(usuario?.Cartoes.Any(x => x.Ativo));
            Assert.True(usuario?.Playlists.Any(x => x.Titulo.Contains("Minha Playlist nº 1") && x.Usuario.Id == usuario.Id));
            Assert.True(pagamento?.Cartao.Transacoes.Any(x => x.Situacao == StatusTransacao.Pago));

        }


        [Fact(DisplayName = "Usuario Adicionar Cartão com sucesso")]
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
