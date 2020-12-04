using System;
using MediatR;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        #region Private Read-Only Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IMediatrHandler Members

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        #endregion
    }
}
