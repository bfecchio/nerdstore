using System;

using NerdStore.Pagamentos.Business.Models;
using NerdStore.Pagamentos.Business.Facades;
using NerdStore.Pagamentos.Business.Enumerations;
using NerdStore.Pagamentos.AntiCorruption.Helpers;
using NerdStore.Pagamentos.AntiCorruption.Gateways;

namespace NerdStore.Pagamentos.AntiCorruption
{
    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade
    {
        #region Private Read-Only Fields

        private readonly IPayPalGateway _payPalGateway;        

        #endregion

        #region Constructors

        public PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway)
        {
            _payPalGateway = payPalGateway ?? throw new ArgumentNullException(nameof(payPalGateway));            
        }

        #endregion

        #region IPagamentoCartaoCreditoFacade Members

        public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
        {
            var apiKey = ConfigHelper.GetValue("apiKey");
            var encKey = ConfigHelper.GetValue("encKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, pagamento.NumeroCartao);

            var pagamentoResultado = _payPalGateway.CommitTransaction(cardHashKey, pedido.Id.ToString(), pagamento.Valor);

            // TODO: informacao retornada pelo gateway
            var transacao = new Transacao(pedido.Id, pagamento.Id, pedido.Valor);

            if (pagamentoResultado)
            {
                transacao.StatusTransacao = StatusTransacao.Pago;
                return transacao;
            }

            transacao.StatusTransacao = StatusTransacao.Recusado;
            return transacao;
        }

        #endregion
    }
}
