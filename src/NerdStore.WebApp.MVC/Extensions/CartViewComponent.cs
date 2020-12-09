using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NerdStore.Vendas.Application.Queries;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        #region Private Constants

        // TODO: Obter usuario logado
        protected Guid ClienteId = Guid.Parse("7030F1D4-A952-4B27-B323-7277615621FB");

        #endregion

        #region Private Read-Only Fields

        private readonly IPedidoQueries _pedidoQueries;

        #endregion

        #region Constructors

        public CartViewComponent(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries ?? throw new ArgumentNullException(nameof(pedidoQueries));
        }

        #endregion

        #region Public Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            var itens = carrinho?.Itens.Count ?? 0;

            return View(itens);
        }

        #endregion
    }
}
