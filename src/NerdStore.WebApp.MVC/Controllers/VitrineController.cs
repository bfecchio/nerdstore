using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class VitrineController : Controller
    {
        #region Private Read-Only Fields

        private readonly IProdutoAppService _produtoAppService;

        #endregion

        #region Constructors

        public VitrineController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService ?? throw new ArgumentNullException(nameof(produtoAppService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        [Route(""), Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoAppService.ObterTodos();
            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            return View(produto);
        }

        #endregion
    }
}
