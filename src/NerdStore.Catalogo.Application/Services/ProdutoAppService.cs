using System;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using NerdStore.Core.DomainObjects;
using NerdStore.Catalogo.Domain.Models;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Application.ViewModels;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        #region Private Read-Only Fields

        private readonly IMapper _mapper;
        private readonly IEstoqueService _estoqueService;
        private readonly IProdutoRepository _produtoRepository;

        #endregion

        #region Constructors

        public ProdutoAppService(IMapper mapper, IEstoqueService estoqueService, IProdutoRepository produtoRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _estoqueService = estoqueService ?? throw new ArgumentNullException(nameof(estoqueService));
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        #endregion

        #region IProdutoAppService Members

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            var colecao = await _produtoRepository.ObterPorCategoria(codigo);
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(colecao);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var colecao = await _produtoRepository.ObterTodos();
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(colecao);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            var colecao = await _produtoRepository.ObterCategorias();
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(colecao);
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            
            _produtoRepository.Adicionar(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            _produtoRepository.Atualizar(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            var resultado = await _estoqueService.DebitarEstoque(id, quantidade);
            if (!resultado)
                throw new DomainException("Falha ao debitar estoque.");

            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            var resultado = await _estoqueService.ReporEstoque(id, quantidade);
            if (!resultado)
                throw new DomainException("Falha ao repor estoque.");

            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public void Dispose()
        {
            _estoqueService?.Dispose();
            _produtoRepository?.Dispose();
        }

        #endregion
    }
}
