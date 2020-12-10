using System;
using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validators
{
    public class CancelarProcessamentoPedidoValidator : AbstractValidator<CancelarProcessamentoPedidoCommand>
    {
        public CancelarProcessamentoPedidoValidator()
        {
            RuleFor(p => p.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(p => p.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido.");
        }
    }
}
