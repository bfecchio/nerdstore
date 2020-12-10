using System;
using System.Collections.Generic;

namespace NerdStore.Pagamentos.Business.Models
{
    public class Pedido
    {

        #region Public Properties

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public List<Produto> Produtos { get; set; }

        #endregion

        #region Constructors

        public Pedido(Guid id, decimal valor)
            : this(id, valor, null)
        { }

        public Pedido(Guid id, decimal valor, List<Produto> produtos)
        {
            Id = id;
            Valor = valor;
            Produtos = produtos ?? new List<Produto>();
        }

        #endregion
    }
}
