using System;
using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validators
{
    public class AplicarVoucherPedidoValidator : AbstractValidator<AplicarVoucherPedidoCommand>
    {
        public AplicarVoucherPedidoValidator()
        {
            RuleFor(p => p.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(p => p.CodigoVoucher)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio.");
        }
    }
}
