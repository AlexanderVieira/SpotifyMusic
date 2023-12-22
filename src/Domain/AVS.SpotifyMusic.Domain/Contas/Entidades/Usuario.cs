using AVS.SpotifyMusic.Domain.Conta.Factories;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Core.Utils;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using FluentValidation;
using AVS.SpotifyMusic.Domain.Core.Notificacoes;
using AVS.SpotifyMusic.Domain.Core.Data;

namespace AVS.SpotifyMusic.Domain.Contas.Entidades
{
    public class Usuario : Entity, IAggregateRoot
    {
        private const string NOME_PLAYLIST = "Minha Playlist";
        private int _numero = 0;

        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public Senha Senha { get; private set; }
        public string? Foto { get; private set; }
        public bool Ativo { get; set; }
        public DateTime DtNascimento { get; private set; }
        public List<Cartao> Cartoes { get; private set; } = new List<Cartao>();
        public List<Assinatura> Assinaturas { get; private set; } = new List<Assinatura>();
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();
        public List<Notificacao> Notificacoes { get; private set; } = new List<Notificacao>();

        public Usuario()
        {            
        }

        public Usuario(string nome, string email, string cpf, string senha, bool ativo, DateTime dtNascimento, string? foto = null)
        {
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Senha = new Senha(senha);
            Foto = foto;
            Ativo = ativo;
            DtNascimento = dtNascimento;
            
        }

        public void CriarConta(Plano plano, Pagamento pagamento)
        {   
            AssinarPlano(plano, pagamento);
            AdicionarCartao(pagamento.Cartao);
            CriarPlaylist(titulo: $"{NOME_PLAYLIST} nº {++_numero}", descricao: "Preencha sua descrição", publico: false);
        }

        public void AssinarPlano(Plano plano, Pagamento pagamento) 
        {
            pagamento.CriarTransacao(pagamento.Cartao, pagamento.Transacao);
            DesativarAssinaturaAtiva();
            CriarAssinatura(plano);
        }

        public void DesativarAssinaturaAtiva()
        {
            if (Assinaturas.Any(x => x.Ativo)) Assinaturas.ForEach(a => { a.Inativar(); });           
        }       

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public void CriarPlaylist(string titulo, string descricao, bool publico, string? foto = null)
        {
            AdicionarPlaylist(new Playlist(titulo, descricao, publico, this, foto));
        }

        public void AdicionarPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist);
        }

        public void AtualizarPlaylist(List<Playlist> playlists)
        {
            Playlists.AddRange(playlists);
        }

        public void RemoverPlaylist(Playlist playlist)
        {
            Playlists.Remove(playlist);
        }

        public void RemoverPlaylists()
        {
            Playlists.Clear();
        }

        public void CriarAssinatura(Plano plano, bool ativo = true)
        {            
            var assinatura = AssinaturaFatory.Criar(plano, ativo);
            AdicionarAssinatura(assinatura);
        }

        public void AtualizarPlano(Plano plano)
        {
            if (Assinaturas.Any()) 
            { 
                Assinatura? assinatura = Assinaturas.LastOrDefault(a => a.Ativo);
                if (assinatura != null)
                {
                    assinatura.AtualizarPlano(plano);
                    AdicionarAssinatura(assinatura);
                }
            }
        }

        public void AdicionarAssinatura(Assinatura assinatura)
        {
            Assinaturas.Add(assinatura);
        }

        public void AdicionarCartao(Cartao cartao)
        {
            Cartoes.Add(cartao);
        }

        public void AdicionarNotificacao(Notificacao notificacao)
        {
            Notificacoes ??= new List<Notificacao>();
            Notificacoes.Add(notificacao);
        }

        public void RemoveNotificacao(Notificacao notificacao)
        {
            Notificacoes?.Remove(notificacao);
        }

        public void LimparNotificacao()
        {
            Notificacoes?.Clear();
        }

        public override bool EhValido()
        {
            ValidationResult = new UsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new UsuarioValidator().ValidateAndThrow(this);
        }
    }

    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Identificador do usuário inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .Length(2, 150)
                .WithMessage("O Nome deve ter entre 2 a 150 caracteres.");

            RuleFor(x => x.Cpf.Numero)
                .NotEmpty()
                .WithMessage("Documento é obrigatório.")
                .Must(Cpf.ValidarCpf)
                .WithMessage("Documento inválido.");

            RuleFor(x => x.Email.Address)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório.")
                .Must(Email.ValidarEmail)
                .WithMessage("E-mail inválido.");

            RuleFor(x => x.Senha.Valor)
               .NotEmpty()
               .WithMessage("Senha é obrigatória.")
               .MinimumLength(8)
               .WithMessage("Senha precisa ter pelo menos 8 caracteres.")
               .Must(Senha.ValidarFormato)
               .WithMessage("Senha inválida.");

            RuleFor(x => x.Senha)
                .Custom((senha, context) => 
                { 
                    if (Senha.CriptografarSenha(senha.Valor) == string.Empty) 
                    { 
                        context.AddFailure("Algo deu errado! A criptografia falhou."); 
                    } 
                });

            RuleFor(x => x.Foto)               
               .MaximumLength(81578)
               .WithMessage("Limite para o comprimento da cadeia de caracteres de consulta excedido.");

            RuleFor(x => x.DtNascimento)                
                .Must(ValidationUtils.BeAValidDate)
                .WithMessage("Data de nascimento não informada.")
                .LessThan(DateTime.Now.AddYears(-18))
                .WithMessage("Usuário menor que 18 anos.");

            RuleFor(x => x.DtNascimento)
                .Custom((dtNascimento, context) =>
                {                                       
                    if (dtNascimento.Date.ToLongDateString() != null)
                    {
                        if (DateUtils.IsDataInformadaMaiorQueDataAtual(dtNascimento.Date.ToShortDateString()))
                            context.AddFailure("A data de nascimento informada não é válida.");
                    }

                });
        }
    }
}
