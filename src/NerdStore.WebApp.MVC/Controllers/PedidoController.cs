using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NerdStore.Vendas.Application.Queries;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class PedidoController : ControllerBase
    {
        #region Private Read-Only Fields

        private readonly IPedidoQueries _pedidoQueries;        

        #endregion

        #region Constructors

        public PedidoController(IPedidoQueries pedidoQueries, IMediatorHandler mediatorHandler, INotificationHandler<DomainNotification> notifications)
            : base(mediatorHandler, notifications)
        {
            _pedidoQueries = pedidoQueries ?? throw new ArgumentNullException(nameof(pedidoQueries));
        }

        #endregion

        #region Controller Actions

        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoQueries.ObterPedidosCliente(ClienteId);
            return View(pedidos);
        }

        #endregion
    }
}
