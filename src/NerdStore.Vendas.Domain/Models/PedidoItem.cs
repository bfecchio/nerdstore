using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain.Models
{
    public class PedidoItem : Entity
    {
        #region Public Properties

        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        #endregion

        #region EF Properties

        public virtual Pedido Pedido { get; private set; }

        #endregion

        #region Constructors

        protected PedidoItem()
        { }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        #endregion

        #region Behaviours

        public decimal CalcularValor()
            => Quantidade * ValorUnitario;

        internal void AssociarPedido(Guid pedidoId)
            => PedidoId = pedidoId;

        internal void AdicionarUnidades(int unidades)
            => Quantidade += unidades;

        internal void AtualizarUnidades(int unidades)
            => Quantidade = unidades;

        public override bool EhValido()
            => true;

        #endregion
    }
}
