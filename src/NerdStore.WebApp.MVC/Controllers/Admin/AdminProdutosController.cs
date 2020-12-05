using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModels;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProdutosController : Controller
    {
        #region Private Read-Only Fields

        private readonly IProdutoAppService _produtoAppService;

        #endregion

        #region Constructors

        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService ?? throw new ArgumentNullException(nameof(produtoAppService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        [Route("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            var colecao = await _produtoAppService.ObterTodos();
            return View(colecao);
        }

        [HttpGet]
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            var produtoViewModel = await PopularCategorias(new ProdutoViewModel());
            return View(produtoViewModel);
        }

        [HttpPost]
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return View(await PopularCategorias(new ProdutoViewModel()));

            await _produtoAppService.AdicionarProduto(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            var produtoViewModel = await PopularCategorias(await _produtoAppService.ObterPorId(id));
            return View(produtoViewModel);
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove(nameof(ProdutoViewModel.QuantidadeEstoque));
            if (!ModelState.IsValid)
                return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AtualizarProduto(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            var produtoViewModel = await _produtoAppService.ObterPorId(id);
            return View("Estoque", produtoViewModel);
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {
            if (quantidade > 0)
                await _produtoAppService.ReporEstoque(id, quantidade);
            else
                await _produtoAppService.DebitarEstoque(id, quantidade);

            var colecao = await _produtoAppService.ObterTodos();
            return View(nameof(Index), colecao);
        }

        #endregion

        #region Private Methods

        public async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Categorias = await _produtoAppService.ObterCategorias();
            return produtoViewModel;
        }

        #endregion
    }
}
