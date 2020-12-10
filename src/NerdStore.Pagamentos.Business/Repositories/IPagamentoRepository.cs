using NerdStore.Core.Data;
using NerdStore.Pagamentos.Business.Models;

namespace NerdStore.Pagamentos.Business.Repositories
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        #region IPagamentoRepository Members

        void Adicionar(Pagamento pagamento);
        void AdicionarTransacao(Transacao transacao);

        #endregion
    }
}
