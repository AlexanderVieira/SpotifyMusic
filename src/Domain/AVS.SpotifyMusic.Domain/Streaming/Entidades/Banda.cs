using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Banda : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string? Foto { get; private set; }
        public virtual ICollection<Album> Albuns { get; private set; } = new List<Album>();

        protected Banda()
        {            
        }

        public Banda(string nome, string descricao, string? foto = null)
        {
            Nome = nome;
            Descricao = descricao;
            Foto = foto;
        }

        public override bool EhValido()
        {
            ValidationResult = new BandaValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new BandaValidator().ValidateAndThrow(this);
        }
    }

    public class BandaValidator : AbstractValidator<Banda> 
    {
        public BandaValidator() 
        {
            RuleFor(x => x.Id)
               .NotEqual(Guid.Empty)
               .WithMessage("Identificador da Banda inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .Length(6, 30)
                .WithMessage("O Nome deve ter entre 6 a 30 caracteres.");

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
