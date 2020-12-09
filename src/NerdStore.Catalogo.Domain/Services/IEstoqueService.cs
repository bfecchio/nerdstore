using System;
using System.Threading.Tasks;

using NerdStore.Core.DomainObjects.Dtos;

namespace NerdStore.Catalogo.Domain.Services
{
    public interface IEstoqueService : IDisposable
    {
        #region IEstoqueService Members

        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista);

        #endregion
    }
}
