using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoFinalizadoEvent : Event
    {
        #region Public Properties

        public Guid PedidoId { get; private set; }

        #endregion

        #region Constructors

        public PedidoFinalizadoEvent(Guid pedidoId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;

        }

        #endregion
    }
}
