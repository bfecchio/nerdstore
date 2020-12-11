using System;
using MediatR;
using System.Threading.Tasks;

using NerdStore.Core.Messages;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;

namespace NerdStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        #region Private Read-Only Fields

        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        #endregion

        #region Constructors

        public MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _eventSourcingRepository = eventSourcingRepository ?? throw new ArgumentNullException(nameof(eventSourcingRepository));
        }

        #endregion

        #region IMediatrHandler Members

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);

            if (!evento.GetType().BaseType.Name.Equals(nameof(DomainEvent)))
                await _eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Command
            => await _mediator.Send(comando);

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
            => await _mediator.Publish(notificacao);

        #endregion
    }
}
