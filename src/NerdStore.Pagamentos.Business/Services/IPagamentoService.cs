using System.Threading.Tasks;

using NerdStore.Core.DomainObjects.Dtos;
using NerdStore.Pagamentos.Business.Models;

namespace NerdStore.Pagamentos.Business.Services
{
    public interface IPagamentoService
    {
        #region IPagamentoService Members

        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);

        #endregion
    }
}
