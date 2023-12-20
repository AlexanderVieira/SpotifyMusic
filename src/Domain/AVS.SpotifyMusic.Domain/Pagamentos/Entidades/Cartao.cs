using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using FluentValidation;

namespace AVS.SpotifyMusic.Domain.Pagamentos.Entidades
{
    public class Cartao : Entity
    {
        public string Numero { get; private set; }
        public string Nome { get; private set; }
        public string Expiracao { get; private set; }
        public string Cvv { get; private set; }
        public bool Ativo { get; private set; }
        public Monetario Limite { get; private set; }
        public Pagamento Pagamento { get; set; }
        public Guid PagamentoId { get; set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        protected Cartao()
        {            
        }

        public Cartao(string numero, string nome, string expiracao, string cvv, bool ativo, decimal limite)
        {
            Numero = numero;
            Nome = nome;
            Expiracao = expiracao;
            Cvv = cvv;
            Ativo = ativo;
            Limite = new Monetario(limite);
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new CartaoValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Validar()
        {
            new CartaoValidator().ValidateAndThrow(this);
        }

        public void AtualizarLimite(Transacao transacao)
        {
            Limite -= transacao.Valor;
        }

        public bool TemLimite(Transacao transacao)
        {
            return Limite >= transacao.Valor;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }
    }

    public class CartaoValidator : AbstractValidator<Cartao>
    {
        public CartaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Identificador do cartão inválido.");

            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("Número é obrigatório.")
                .Length(19)
                .WithMessage("Número inválido.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .Length(6, 30)
                .WithMessage("O Nome deve ter entre 6 a 30 caracteres.");

            RuleFor(x => x.Expiracao)
                .NotEmpty()
                .WithMessage("Data de Expiração é obrigatória.")
                .Length(7)
                .WithMessage("Data de Expiração inválida.");

            RuleFor(x => x.Cvv)
                .NotEmpty()
                .WithMessage("Cvv é obrigatório.")
                .Length(3)
                .WithMessage("Cvv inválido.");

            RuleFor(x => x.Limite.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do limite não pode ser negativo.");               

            RuleFor(x => x.Ativo)
                .Equal(true)
                .WithMessage("Cartão não está ativo.");

        }
        
    }
}
