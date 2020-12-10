using System;

namespace NerdStore.Core.DomainObjects.Dtos
{
    public class PagamentoPedido
    {
        #region Public Properties

        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal Total { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CVVCartao { get; set; }

        #endregion

        #region Constructors

        public PagamentoPedido(Guid pedidoId, Guid clienteId, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CVVCartao = cvvCartao;
        }

        #endregion
    }
}
