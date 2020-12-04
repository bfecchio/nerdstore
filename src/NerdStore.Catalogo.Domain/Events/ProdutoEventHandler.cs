using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        #region Private Read-Only Fields
        
        private readonly IProdutoRepository _produtoRepository;

        #endregion

        #region Constructors

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
        {            
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(mensagem.AggregateId);
            
            // criar uma solicitação de compra
            // notificar departamento de compras por e-mail
        }

        #endregion
    }
}
