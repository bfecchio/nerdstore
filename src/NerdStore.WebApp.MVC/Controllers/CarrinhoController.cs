using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NerdStore.Vendas.Application.Queries;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Queries.ViewModels;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        #region Private Read-Only Fields

        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoAppService _produtoAppService;

        #endregion

        #region Constructors

        public CarrinhoController(
            IMediatorHandler mediatorHandler,
            INotificationHandler<DomainNotification> notifications,
            IProdutoAppService produtoAppService,
            IPedidoQueries pedidoQueries
        )
            : base(mediatorHandler, notifications)
        {
            _pedidoQueries = pedidoQueries ?? throw new ArgumentNullException(nameof(pedidoQueries));
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
            _produtoAppService = produtoAppService ?? throw new ArgumentNullException(nameof(produtoAppService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            var colecao = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            return View(colecao);
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente!";
                return RedirectToAction(nameof(VitrineController.ProdutoDetalhe), "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction(nameof(VitrineController.ProdutoDetalhe), "Vitrine", new { id });
        }

        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new RemoverItemPedidoCommand(ClienteId, id);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return RedirectToAction(nameof(Index));

            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            return View(nameof(Index), carrinho);
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return RedirectToAction(nameof(Index));

            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            return View(nameof(Index), carrinho);
        }

        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return RedirectToAction(nameof(Index));

            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            return View(nameof(Index), carrinho);
        }

        [HttpGet]
        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            return View(carrinho);
        }


        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);

            var command = new IniciarPedidoCommand(carrinho.PedidoId, ClienteId, carrinho.ValorTotal,
                carrinhoViewModel.Pagamento.NomeCartao, carrinhoViewModel.Pagamento.NumeroCartao,
                carrinhoViewModel.Pagamento.ExpiracaoCartao, carrinhoViewModel.Pagamento.CVVCartao
            );
            
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return RedirectToAction(nameof(Index), "Pedido");

            return View("ResumoDaCompra", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        #endregion
    }
}
