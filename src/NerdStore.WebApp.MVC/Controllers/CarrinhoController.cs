using System;
using NerdStore.Core.Bus;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        #region Private Read-Only Fields

        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoAppService _produtoAppService;

        #endregion

        #region Constructors

        public CarrinhoController(IMediatorHandler mediatorHandler, IProdutoAppService produtoAppService)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
            _produtoAppService = produtoAppService ?? throw new ArgumentNullException(nameof(produtoAppService));
        }

        #endregion

        #region Controller Actions

        public IActionResult Index()
        {
            return View();
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
                return RedirectToAction(nameof(VitrineController.ProdutoDetalhe), nameof(VitrineController), new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatorHandler.EnviarComando(command);

            TempData["Erro"] = "Produto indisponível!";
            return RedirectToAction(nameof(VitrineController.ProdutoDetalhe), nameof(VitrineController), new { id });
        }

        #endregion
    }
}
