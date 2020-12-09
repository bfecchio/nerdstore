using System;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoEstoqueRejeitadoEvent : IntegrationEvent
    {
        #region Public Properties

        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }

        #endregion

        #region Constructors

        public PedidoEstoqueRejeitadoEvent(Guid pedidoId, Guid clienteId)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }

        #endregion
    }
}
