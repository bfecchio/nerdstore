using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoVoucherAplicadoEvent : Event
    {
        #region Public Properties

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid VoucherId { get; private set; }        

        #endregion

        #region Constructors

        public PedidoVoucherAplicadoEvent(Guid clienteId, Guid pedidoId, Guid voucherId)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
            VoucherId = voucherId;            
        }

        #endregion
    }
}
