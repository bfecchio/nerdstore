using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        #region Private Read-Only Fields

        private readonly IProdutoRepository _produtoRepository;

        #endregion

        #region Constructors

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        #endregion

        #region IEstoqueService Members

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            if (!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);
            
            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);
            if (produto == null) return false;
            
            produto.ReporEstoque(quantidade);
            
            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }

        #endregion
    }
}
