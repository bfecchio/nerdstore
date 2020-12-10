using NerdStore.Pagamentos.Business.Models;

namespace NerdStore.Pagamentos.Business.Facades
{
    public interface IPagamentoCartaoCreditoFacade
    {
        #region IPagamentoCartaoCreditoFacade Members

        Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);

        #endregion
    }
}
