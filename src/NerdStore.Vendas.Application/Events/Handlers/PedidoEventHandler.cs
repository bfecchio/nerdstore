using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events.Handlers
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>
    {
        #region INotificationHandler Members

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
            => Task.CompletedTask;    

        #endregion
    }
}
