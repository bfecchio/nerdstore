using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

using NerdStore.Catalogo.Domain.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler :
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<PedidoIniciadoEvent>,
        INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        #region Private Read-Only Fields

        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;

        #endregion

        #region Constructors

        public ProdutoEventHandler(IEstoqueService estoqueService, IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler)
        {
            _estoqueService = estoqueService ?? throw new ArgumentNullException(nameof(estoqueService));
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);
            
            // criar uma solicitação de compra
            // notificar departamento de compras por e-mail
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
        {
            var resultado = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

            if (resultado)
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(message.PedidoId, message.ClienteId, message.Total,
                    message.ProdutosPedido, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CVVCartao));
            else
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent message, CancellationToken cancellationToken)
            => await _estoqueService.ReporListaProdutosPedido(message.ProdutosPedido);

        #endregion
    }
}
