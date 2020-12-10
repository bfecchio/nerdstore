using System;
using System.Threading.Tasks;

using NerdStore.Core.DomainObjects.Dtos;
using NerdStore.Pagamentos.Business.Models;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Pagamentos.Business.Facades;
using NerdStore.Pagamentos.Business.Repositories;
using NerdStore.Pagamentos.Business.Enumerations;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Pagamentos.Business.Services
{
    public class PagamentoService : IPagamentoService
    {
        #region Private Read-Only Fields

        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;

        #endregion

        #region Constructors

        public PagamentoService(IMediatorHandler mediatorHandler, IPagamentoRepository pagamentoRepository, IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
            _pagamentoRepository = pagamentoRepository ?? throw new ArgumentNullException(nameof(pagamentoRepository));
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade ?? throw new ArgumentNullException(nameof(pagamentoCartaoCreditoFacade));
        }

        #endregion

        #region IPagamentoService Members

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido(pagamentoPedido.PedidoId, pagamentoPedido.Total);
            var pagamento = new Pagamento(pagamentoPedido.Total, pagamentoPedido.NomeCartao,
                pagamentoPedido.NumeroCartao, pagamentoPedido.ExpiracaoCartao, pagamentoPedido.CVVCartao, pagamentoPedido.PedidoId);

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

                _pagamentoRepository.Adicionar(pagamento);
                _pagamentoRepository.AdicionarTransacao(transacao);
                
                await _pagamentoRepository.UnitOfWork.Commit();
                
                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento."));
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.ClienteId, transacao.PagamentoId, transacao.Id, pedido.Valor));

            return transacao;
        }

        #endregion
    }
}
