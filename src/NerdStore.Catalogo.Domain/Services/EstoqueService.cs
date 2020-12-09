using System;
using System.Threading.Tasks;

using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.DomainObjects.Dtos;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        #region Private Read-Only Fields

        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;

        #endregion

        #region Constructors

        public EstoqueService(IMediatorHandler bus, IProdutoRepository produtoRepository)
        {
            _mediatorHandler = bus ?? throw new ArgumentNullException(nameof(bus));
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        #endregion

        #region IEstoqueService Members

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);
            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
                await ReporItemEstoque(item.Id, item.Quantidade);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
            => _produtoRepository?.Dispose();

        #endregion

        #region Private Methods

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque."));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            if (produto.QuantidadeEstoque < 10)
                await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));

            _produtoRepository.Atualizar(produto);
            return true;
        }

        public async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);
            if (produto == null) return false;

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return true;
        }

        #endregion
    }
}
