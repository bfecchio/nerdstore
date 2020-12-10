using System;

using NerdStore.Core.DomainObjects;

namespace NerdStore.Pagamentos.Business.Models
{
    public class Pagamento : Entity, IAggregateRoot
    {
        #region Public Properties

        public Guid PedidoId { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CVVCartao { get; set; }

        public virtual Transacao Transacao { get; set; }

        #endregion

        #region Constructors

        protected Pagamento()
        { }

        public Pagamento(decimal valor, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao, Guid pedidoId)
        {
            Valor = valor;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CVVCartao = cvvCartao;
            PedidoId = pedidoId;
        }

        #endregion
    }
}
