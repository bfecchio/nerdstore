using System;
using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validators
{
    public class AtualizarItemPedidoValidator : AbstractValidator<AtualizarItemPedidoCommand>
    {
        public AtualizarItemPedidoValidator()
        {
            RuleFor(p => p.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(p => p.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido.");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade mínima de um item é 1.");

            RuleFor(c => c.Quantidade)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15.");
        }
    }
}
