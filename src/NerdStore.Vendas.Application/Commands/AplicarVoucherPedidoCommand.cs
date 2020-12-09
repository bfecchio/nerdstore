using System;

using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validators;

namespace NerdStore.Vendas.Application.Commands
{
    public class AplicarVoucherPedidoCommand : Command
    {
        #region Public Properties

        public Guid ClienteId { get; private set; }
        public string CodigoVoucher { get; private set; }

        #endregion

        #region Constructors

        public AplicarVoucherPedidoCommand(Guid clienteId, string codigoVoucher)
        {            
            ClienteId = clienteId;         
            CodigoVoucher = codigoVoucher;
        }

        #endregion

        #region Overriden Methods

        public override bool EhValido()
        {
            ValidationResult = new AplicarVoucherPedidoValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        #endregion
    }
}
