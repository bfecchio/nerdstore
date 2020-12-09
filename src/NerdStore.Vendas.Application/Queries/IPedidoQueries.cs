using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NerdStore.Vendas.Application.Queries.ViewModels;

namespace NerdStore.Vendas.Application.Queries
{
    public interface IPedidoQueries
    {
        #region IPedidoQueries Members

        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);

        #endregion
    }
}
