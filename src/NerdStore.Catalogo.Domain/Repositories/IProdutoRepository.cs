using System;
using NerdStore.Core.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using NerdStore.Catalogo.Domain.Models;

namespace NerdStore.Catalogo.Domain.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        #region IProdutoRepository Members

        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);

        #endregion
    }
}
