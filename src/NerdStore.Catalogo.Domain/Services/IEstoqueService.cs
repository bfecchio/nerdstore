using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Services
{
    public interface IEstoqueService : IDisposable
    {
        #region IEstoqueService Members

        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);

        #endregion
    }
}
