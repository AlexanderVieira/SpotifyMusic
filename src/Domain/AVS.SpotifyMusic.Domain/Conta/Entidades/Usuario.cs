using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Core.Utils;
using AVS.SpotifyMusic.Domain.Transacao.Entidades;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Conta.Entidades
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public Senha Senha { get; private set; }
        public string Foto { get; private set; }
        public bool Ativo { get; set; }
        public DateTime? DtNascimento { get; private set; }
        public List<Cartao> Cartoes { get; private set; } = new List<Cartao>();
        public List<Assinatura> Assinaturas { get; private set; } = new List<Assinatura>();
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();

        public Usuario(string nome, string email, string cpf, string senha, string foto, bool ativo, DateTime? dtNascimento)
        {
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Senha = new Senha(senha);
            Foto = foto;
            Ativo = ativo;
            DtNascimento = dtNascimento;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
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
                .WithMessage("Id do usuário inválido.");

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
               .WithMessage("Senha é obrigatório.");            

            RuleFor(x => x.DtNascimento)
                .Must(ValidationUtils.HasValidBirthDate)
                .WithMessage("Data de nascimento não informada.");

            RuleFor(x => x.DtNascimento)
                .Custom((dtNascimento, context) =>
                {
                    //if (!string.IsNullOrWhiteSpace(dtNascimento.Value.Date.ToShortDateString()))
                    if (dtNascimento != null)
                    {
                        if (DateUtils.IsDataInformadaMaiorQueDataAtual(dtNascimento.Value.Date.ToShortDateString()))
                            context.AddFailure("A data de nascimento informada não é válida.");
                    }
                });
        }
    }
}
