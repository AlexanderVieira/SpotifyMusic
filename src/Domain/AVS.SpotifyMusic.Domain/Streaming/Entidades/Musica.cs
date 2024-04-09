using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.ObjValor;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Musica : Entity
    {
        public string Nome { get; private set; }
        public Duracao Duracao { get; private set; }
        public virtual ICollection<Playlist> Playlists { get; private set; } = new List<Playlist>();

        protected Musica()
        {            
        }

        public Musica(string nome, int duracao)
        {
            Nome = nome;
            Duracao = new Duracao(duracao);
        }

        public override bool EhValido()
        {
            ValidationResult = new MusicaValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new MusicaValidator().ValidateAndThrow(this);
        }
    }

    public class MusicaValidator : AbstractValidator<Musica>
    {
        public MusicaValidator()
        {
            RuleFor(x => x.Id)
               .NotEqual(Guid.Empty)
               .WithMessage("Identificador da Música inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .Length(6, 30)
                .WithMessage("O Nome deve ter entre 6 a 30 caracteres.");

            RuleFor(x => x.Duracao)
               .NotEmpty()
               .WithMessage("Duração é obrigatória.");

        }
    }

}
