using System;

using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validators;

namespace NerdStore.Vendas.Application.Commands
{
    public class RemoverItemPedidoCommand : Command
    {
        #region Public Properties

        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }

        #endregion

        #region Constructors

        public RemoverItemPedidoCommand(Guid clienteId, Guid produtoId)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;            
        }

        #endregion

        #region Overriden Methods

        public override bool EhValido()
        {
            return base.EhValido();
        }

        #endregion
    }
}
