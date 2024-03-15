using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Enums;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;

namespace AVS.SpotifyMusic.Domain.Core.Notificacoes
{
    public class Notificacao : Entity
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public virtual Usuario Destino { get; set; }
        public virtual Usuario? Remetente { get; set; }
        public TipoNotificacao TipoNotificacao { get; set; }

        protected Notificacao()
        {            
        }

        public Notificacao(string titulo, string mensagem, TipoNotificacao tipoNotificacao, Usuario destino, Usuario? remetente = null)
        {
            if (tipoNotificacao == TipoNotificacao.Usuario && remetente == null)
                throw new DomainException("Para tipo de mensagem 'usuário', você deve informar quem foi o remetente");

            if (string.IsNullOrWhiteSpace(titulo))
                throw new DomainException("Informe o titulo da notificacao");

            if (string.IsNullOrWhiteSpace(mensagem))
                throw new DomainException("Informe o mensagem da notificacao");

            Titulo = titulo;
            Mensagem = mensagem;
            Destino = destino;
            Remetente = remetente;
            TipoNotificacao = tipoNotificacao;
        }
    }
}
