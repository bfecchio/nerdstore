using System;
using NerdStore.Core.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using NerdStore.Vendas.Domain.Models;

namespace NerdStore.Vendas.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        #region IPedidoRepository Members

        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);

        Task<PedidoItem> ObterItemPorId(Guid id);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);

        Task<Voucher> ObterVoucherPorCodigo(string codigo);

        #endregion
    }
}
