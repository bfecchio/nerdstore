using MediatR;
using System.Threading;
using System.Threading.Tasks;

using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Vendas.Application.Events.Handlers
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoItemAtualizadoEvent>,
        INotificationHandler<PedidoItemRemovidoEvent>,
        INotificationHandler<PedidoVoucherAplicadoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>
    {
        #region INotificationHandler Members

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoItemAtualizadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoItemRemovidoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoVoucherAplicadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public async Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            // cancelar o processamento do pedido
            // retornar erro para o cliente

            throw new System.NotImplementedException();
        }

        #endregion
    }
}
