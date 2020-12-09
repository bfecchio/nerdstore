using System;
using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validators
{
    public class RemoverItemPedidoValidator : AbstractValidator<RemoverItemPedidoCommand>
    {
        public RemoverItemPedidoValidator()
        {
            RuleFor(p => p.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(p => p.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido.");
        }
    }
}
