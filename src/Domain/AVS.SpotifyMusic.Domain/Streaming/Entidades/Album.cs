using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Album : Entity
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string? Foto { get; private set; }
        public virtual ICollection<Musica> Musicas { get; private set; } = new List<Musica>();

        protected Album()
        {            
        }

        public Album(string titulo, string descricao, string? foto = null)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
        }

        public override bool EhValido()
        {
            ValidationResult = new AlbumValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new AlbumValidator().ValidateAndThrow(this);
        }
    }

    public class AlbumValidator : AbstractValidator<Album>
    {
        public AlbumValidator()
        {
            RuleFor(x => x.Id)
               .NotEqual(Guid.Empty)
               .WithMessage("Identificador da Banda inválido.");

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("Titulo é obrigatório.")
                .Length(6, 30)
                .WithMessage("O Titulo deve ter entre 6 a 30 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descricao é obrigatório.")
                .Length(6, 150)
                .WithMessage("Descrição deve ter entre 6 a 150 caracteres.");

            RuleFor(x => x.Foto)
               .MaximumLength(150)
               .WithMessage("Url da foto deve ter no máximo 150 caracteres.");
        }
    }
}
