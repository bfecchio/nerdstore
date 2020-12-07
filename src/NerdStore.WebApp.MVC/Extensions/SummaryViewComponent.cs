using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        #region Private Read-Only Fields

        private readonly DomainNotificationHandler _notifications;

        #endregion

        #region Constructors

        public SummaryViewComponent(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        #endregion

        #region Public Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifications.ObterNotificacoes());
            notificacoes.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x.Value));

            return View();
        }

        #endregion
    }
}
