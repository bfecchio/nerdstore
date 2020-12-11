using System;

namespace NerdStore.Vendas.Application.Queries.ViewModels
{
    public class PedidoViewModel
    {
        #region Public Properties

        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PedidoStatus { get; set; }

        #endregion
    }
}
