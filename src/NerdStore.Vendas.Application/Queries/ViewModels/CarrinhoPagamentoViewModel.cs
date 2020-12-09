namespace NerdStore.Vendas.Application.Queries.ViewModels
{
    public class CarrinhoPagamentoViewModel
    {
        #region Public Properties

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CVVCartao { get; set; }

        #endregion
    }
}
