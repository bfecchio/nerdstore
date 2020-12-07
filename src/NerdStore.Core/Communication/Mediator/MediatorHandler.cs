using System;
using MediatR;
using System.Threading.Tasks;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        #region Private Read-Only Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IMediatrHandler Members

        public async Task PublicarEvento<T>(T evento) where T : Event
            => await _mediator.Publish(evento);

        public async Task<bool> EnviarComando<T>(T comando) where T : Command
            => await _mediator.Send(comando);

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
            => await _mediator.Publish(notificacao);

        #endregion
    }
}
