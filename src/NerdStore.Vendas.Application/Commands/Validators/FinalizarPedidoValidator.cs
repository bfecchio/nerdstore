using System;
using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validators
{
    public class FinalizarPedidoValidator : AbstractValidator<FinalizarPedidoCommand>
    {
        public FinalizarPedidoValidator()
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
