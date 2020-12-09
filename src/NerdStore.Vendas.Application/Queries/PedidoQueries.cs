using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using NerdStore.Vendas.Domain.Enumerations;
using NerdStore.Vendas.Domain.Repositories;
using NerdStore.Vendas.Application.Queries.ViewModels;

namespace NerdStore.Vendas.Application.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        #region Private Read-Only Fields

        private readonly IPedidoRepository _pedidoRepository;

        #endregion

        #region Constructors

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
        }

        #endregion

        #region IPedidoQueries Members

        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            if (pedido == null) return null;

            var carrinho = new CarrinhoViewModel
            {
                ClienteId = pedido.ClienteId,
                ValorTotal = pedido.ValorTotal,
                PedidoId = pedido.Id,
                ValorDesconto = pedido.Desconto,
                SubTotal = pedido.Desconto + pedido.ValorTotal
            };

            if (pedido.VoucherUtilizado)
                carrinho.VoucherCodigo = pedido.Voucher.Codigo;

            foreach (var item in pedido.PedidoItens)
            {
                var carrinhoItem = new CarrinhoItemViewModel
                {
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorUnitario * item.Quantidade
                };

                carrinho.Itens.Add(carrinhoItem);
            }

            return carrinho;
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId)
        {
            var pedidos = await _pedidoRepository.ObterListaPorClienteId(clienteId);

            pedidos = pedidos.Where(x => x.PedidoStatus == PedidoStatus.Pago || x.PedidoStatus == PedidoStatus.Cancelado)
                .OrderByDescending(x => x.Codigo);

            if (!pedidos.Any()) return null;

            var colecao = new List<PedidoViewModel>();

            foreach (var pedido in pedidos)
            {
                colecao.Add(new PedidoViewModel
                {
                    ValorTotal = pedido.ValorTotal,
                    PedidoStatus = (int)pedido.PedidoStatus,
                    Codigo = pedido.Codigo,
                    DataCadastro = pedido.DataCadastro
                });
            }

            return colecao;
        }

        #endregion
    }
}
