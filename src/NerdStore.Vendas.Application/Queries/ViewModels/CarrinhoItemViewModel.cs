using System;

namespace NerdStore.Vendas.Application.Queries.ViewModels
{
    public class CarrinhoItemViewModel
    {
        #region Public Properties

        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        #endregion
    }
}
