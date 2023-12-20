using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Pagamentos.Enums;
using FluentValidation;
using FluentValidation.Results;

namespace AVS.SpotifyMusic.Domain.Pagamentos.Entidades
{
    public class Pagamento : Entity
    {
        private const int INTERVALO_TRANSACAO = -2;
        private const int REPETICAO_TRANSACAO_MERCHANT = 1;
        private const int LIMITE_TRANSACOES = 3;

        public Monetario Valor { get; private set; }
        public StatusPagamento Situacao { get; private set; }
        public Cartao Cartao { get; private set; }
        public Transacao Transacao { get; private set; }        

        protected Pagamento()
        {            
        }

        public Pagamento(decimal valor, StatusPagamento situacao, Cartao cartao, Transacao transacao)
        {
            Valor = new Monetario(valor);
            Situacao = situacao; 
            Cartao = cartao;
            Transacao = transacao;
            if (!EhValido()) return;
            //CriarTransacao(Cartao, Transacao);
        }

        public override bool EhValido()
        {
            ValidationResult = new PagamentoValidator().Validate(this);
            return ValidationResult.IsValid;
        }        

        public bool OperacaoValida()
        {            
            return ValidationResult.IsValid;
        }
        public override void Validar()
        {
            new PagamentoValidator().ValidateAndThrow(this);
        }

        public void CriarTransacao(Cartao cartao, Transacao transacao)
        {            
            IsCartaoAtivo();            
            VerificarLimiteCartao(cartao, transacao);            
            ValidarTransacao(cartao, transacao);
            
            if (!OperacaoValida()) return;
            
            Cartao.AtualizarLimite(transacao);            
            Cartao.AdicionarTransacao(transacao);
            
        }

        private void ValidarTransacao(Cartao cartao, Transacao transacao)
        {                      

            ValidarLimiteTransacoesExecedido(ObterUltimasTransacoes(cartao).Count());
            
            ValidarTransacaoRepetidaPorMerchant(ObterUltimasTransacoes(cartao), transacao);          

        }

        public List<Transacao> ObterUltimasTransacoes(Cartao cartao)
        {
            var ultimasTransacoes = cartao.Transacoes
                .Where(x => x.DtCriacao >= DateTime.Now.AddMinutes(INTERVALO_TRANSACAO)).ToList();
            return ultimasTransacoes;
        }

        public void VerificarLimiteCartao(Cartao cartao, Transacao transacao)
        {
           if (!cartao.TemLimite(transacao))
            {
                AdicionarErro("Cartão não possui limite para esta transação.");
            }
           
        }

        public void ValidarLimiteTransacoesExecedido(int quantidade)
        {
            if (quantidade >= LIMITE_TRANSACOES)
            {
                AdicionarErro("Cartão utilizado muitas vezes em um período curto.");                
            } 
                         
        }

        public void ValidarTransacaoRepetidaPorMerchant(List<Transacao> ultimasTransacoes, Transacao transacao)
        {
            var resultado = ultimasTransacoes.Where(x => 
                            x.Merchant.Nome.ToUpper() == transacao.Merchant.Nome.ToUpper() && 
                            x.Valor == transacao.Valor).Count() == REPETICAO_TRANSACAO_MERCHANT;
            
            if (resultado)
            {
                AdicionarErro("Transacao Duplicada para o mesmo cartão e o mesmo Comerciante.");                
            }
                
        }

        private void AdicionarErro(string mensagem)
        {
            ValidationResult?.Errors.Add(new ValidationFailure() { ErrorMessage = mensagem });
        }

        private void IsCartaoAtivo()
        {
            if (Cartao.Ativo == false)
            {
                Cartao.EhValido();
            }
        }
    }

    public class PagamentoValidator : AbstractValidator<Pagamento>
    {
        public PagamentoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Identificador do Pagamento inválido.");

            RuleFor(x => x.Valor.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do limite não pode ser negativo.");

            RuleFor(x => x.Cartao)
                .NotNull()
                .WithMessage("Cartão não foi informado.");

            RuleFor(x => x.Transacao)
                .NotNull()
                .WithMessage("Transação não foi informada.");
            
        }

    }
    
}
