using System;
using MediatR;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        #region Private Read-Only Fields

        private readonly IMediatorHandler _mediatorHandler;
        private readonly DomainNotificationHandler _notifications;

        #endregion

        #region Protected Properties

        public Guid ClienteId => Guid.Parse("7030F1D4-A952-4B27-B323-7277615621FB");

        #endregion

        #region Constructors

        protected ControllerBase(IMediatorHandler mediatorHandler, INotificationHandler<DomainNotification> notifications)
        {
            _mediatorHandler = mediatorHandler;
            _notifications = (DomainNotificationHandler)notifications;
        }

        #endregion

        #region Behaviours

        protected bool OperacaoValida()
            => !_notifications.TemNotificacao();

        protected IEnumerable<string> ObterMensagensErro()
            => _notifications.ObterNotificacoes().Select(x => x.Value).ToList();

        protected void NotificarErro(string codigo, string mensagem)
            => _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));

        #endregion
    }
}
