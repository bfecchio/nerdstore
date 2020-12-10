using System;

using NerdStore.Core.DomainObjects;
using NerdStore.Pagamentos.Business.Enumerations;

namespace NerdStore.Pagamentos.Business.Models
{
    public class Transacao : Entity
    {
        #region Public Properties

        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public decimal Total { get; set; }
        public StatusTransacao StatusTransacao { get; set; }

        public virtual Pagamento Pagamento { get; set; }

        #endregion

        #region Constructors

        protected Transacao()
        { }

        public Transacao(Guid pedidoId, Guid pagamentoId, decimal total)
        {
            PedidoId = pedidoId;
            PagamentoId = pagamentoId;
            Total = total;
        }

        #endregion
    }
}
