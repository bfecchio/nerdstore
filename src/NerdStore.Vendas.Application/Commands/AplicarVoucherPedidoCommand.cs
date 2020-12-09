using System;

using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validators;

namespace NerdStore.Vendas.Application.Commands
{
    public class AplicarVoucherPedidoCommand : Command
    {
        #region Public Properties

        public Guid ClienteId { get; private set; }
        public string VoucherCodigo { get; private set; }

        #endregion

        #region Constructors

        public AplicarVoucherPedidoCommand(Guid clienteId, string voucherCodigo)
        {
            ClienteId = clienteId;
            VoucherCodigo = voucherCodigo;
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
