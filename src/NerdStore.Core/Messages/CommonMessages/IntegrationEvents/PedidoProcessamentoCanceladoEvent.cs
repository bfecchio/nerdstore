using System;

using NerdStore.Core.DomainObjects.Dtos;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoProcessamentoCanceladoEvent : IntegrationEvent
    {
        #region Public Properties

        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }        
        public ListaProdutosPedido ProdutosPedido { get; private set; }

        #endregion

        #region Constructors

        public PedidoProcessamentoCanceladoEvent(Guid pedidoId, Guid clienteId, ListaProdutosPedido produtosPedido)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ProdutosPedido = produtosPedido;
        }

        #endregion
    }
}
