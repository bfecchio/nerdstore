using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

using NerdStore.Core.Communication.Mediator;
using NerdStore.Vendas.Application.Commands;
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
        INotificationHandler<PedidoEstoqueRejeitadoEvent>,
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>
    {
        #region Private Read-Only Fields

        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructors

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }

        #endregion

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
            => await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(notification.PedidoId, notification.ClienteId));

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
            => await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(notification.PedidoId, notification.ClienteId));

        public async Task Handle(PagamentoRecusadoEvent notification, CancellationToken cancellationToken)
            => await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(notification.PedidoId, notification.ClienteId));

        #endregion
    }
}
