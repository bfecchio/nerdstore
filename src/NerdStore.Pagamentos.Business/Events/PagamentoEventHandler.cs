using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

using NerdStore.Core.DomainObjects.Dtos;
using NerdStore.Pagamentos.Business.Services;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Pagamentos.Business.Events
{
    public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
    {
        #region Private Read-only Fields

        private readonly IPagamentoService _pagamentoService;

        #endregion

        #region Constructors

        public PagamentoEventHandler(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService ?? throw new ArgumentNullException(nameof(pagamentoService));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(PedidoEstoqueConfirmadoEvent message, CancellationToken cancellationToken)
        {
            var pagamentoPedido = new PagamentoPedido(message.PedidoId, message.ClienteId, message.Total,
                message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CVVCartao);

            await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
        }

        #endregion
    }
}
