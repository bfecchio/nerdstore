using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NerdStore.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        #region Private Fields

        private List<DomainNotification> _notifications;

        #endregion

        #region Constructors

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        #endregion

        #region Public Methods

        public virtual List<DomainNotification> ObterNotificacoes()
            => _notifications;

        public virtual bool TemNotificacao()
            => ObterNotificacoes().Any();

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

        #endregion
    }
}
