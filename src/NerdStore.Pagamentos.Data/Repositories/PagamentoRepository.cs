using System;

using NerdStore.Core.Data;
using NerdStore.Pagamentos.Data.Context;
using NerdStore.Pagamentos.Business.Models;
using NerdStore.Pagamentos.Business.Repositories;

namespace NerdStore.Pagamentos.Data.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        #region Private Read-Only Fields

        private readonly PagamentoContext _context;

        #endregion

        #region Constructors

        public PagamentoRepository(PagamentoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IPagamentoRepository Members

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Pagamento pagamento)
            => _context.Pagamentos.Add(pagamento);

        public void AdicionarTransacao(Transacao transacao)
            => _context.Transacoes.Add(transacao);

        public void Dispose()
            => _context?.Dispose();

        #endregion
    }
}
