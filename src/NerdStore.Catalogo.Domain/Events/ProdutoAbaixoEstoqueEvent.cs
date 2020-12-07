using System;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        #region Public Properties

        public int QuantidadeRestante { get; private set; }

        #endregion

        #region Constructors

        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante)
            : base(aggregateId)
        {
            QuantidadeRestante = quantidadeRestante;
        }

        #endregion
    }
}
