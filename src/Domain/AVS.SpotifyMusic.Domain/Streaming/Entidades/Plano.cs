using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Streaming.Enums;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Plano : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Monetario Valor { get; private set; }
        public TipoPlano TipoPlano { get; private set; }

        public Plano(string nome, string descricao, decimal valor, TipoPlano tipoPlano)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = new Monetario(valor);
            TipoPlano = tipoPlano;            
        }

        public override bool EhValido()
        {
            ValidationResult = new PlanoValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new PlanoValidator().ValidateAndThrow(this);
        }

    }

    public class PlanoValidator : AbstractValidator<Plano>
    {
        public PlanoValidator()
        {
            RuleFor(x => x.Id)
               .NotEqual(Guid.Empty)
               .WithMessage("Identificador do Plano inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .Length(6, 30)
                .WithMessage("O Nome deve ter entre 6 a 30 caracteres.");

            RuleFor(x => x.Descricao)
               .Length(6, 150)
               .WithMessage("Descrição deve ter entre 6 a 150 caracteres.");

            RuleFor(x => x.Valor.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor não pode ser negativo.");

            RuleFor(x => x.TipoPlano)
                .NotEmpty()
                .WithMessage("Tipo do plano é obrigatório.");

        }
    }
}
