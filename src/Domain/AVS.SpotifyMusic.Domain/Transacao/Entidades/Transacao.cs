using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Core.Utils;
using AVS.SpotifyMusic.Domain.Transacao.Enums;
using AVS.SpotifyMusic.Domain.Transacao.ObjValor;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Transacao.Entidades
{
    public class Transacao : Entity
    {
        public DateTime DtTransacao { get; private set; }
        public Monetario Valor { get; private set; }
        public Merchant Merchant { get; private set; }
        public string? Descricao { get; private set; }
        public StatusTransacao Situacao { get; private set; }        

        public Transacao(DateTime dtTransacao, decimal valor, string merchantName, StatusTransacao situacao, string? descricao = null)
        {
            DtTransacao = dtTransacao;
            Valor = new Monetario(valor);
            Merchant = new Merchant(merchantName);
            Descricao = descricao;
            Situacao = situacao;
            if (!EhValido()) return;
        }

        public override bool EhValido()
        {
            ValidationResult = new TransacaoValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new TransacaoValidator().ValidateAndThrow(this);
        }
    }

    public class TransacaoValidator : AbstractValidator<Transacao>
    {
        public TransacaoValidator() 
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Identificador da transação inválido.");

            RuleFor(x => x.Valor.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor não pode ser negativo.");

            RuleFor(x => x.Merchant.Nome)
               .NotEmpty()
               .WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Descricao)
               .Length(6, 150)
               .WithMessage("Descrição deve ter entre 6 a 150 caracteres.");

            RuleFor(x => x.Situacao)
                .NotEmpty()
                .WithMessage("Situação da transação é obrigatória.");

            RuleFor(x => x.DtTransacao.ToShortDateString())
                .NotEmpty()
                .WithMessage("Data da transação é obrigatória.");

            RuleFor(x => x.DtTransacao)
                .Custom((dtTran, context) =>
                {
                    if (dtTran.Date.ToShortDateString() != null)
                    {
                        if (DateUtils.IsDataInformadaMaiorQueDataAtual(dtTran.Date.ToShortDateString()))
                            context.AddFailure("A data da transação informada não é válida.");
                    }
                });
        }
    }
}
