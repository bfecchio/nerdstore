using System;
using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validators
{
    public class IniciarPedidoValidator : AbstractValidator<IniciarPedidoCommand>
    {
        public IniciarPedidoValidator()
        {
            RuleFor(p => p.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(p => p.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido.");

            RuleFor(p => p.NomeCartao)
                .NotEmpty()
                .WithMessage("O nome do cartão não foi informado.");

            RuleFor(p => p.NumeroCartao)
                .NotEmpty()
                .WithMessage("O número do cartão não foi informado.");

            RuleFor(p => p.NumeroCartao)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido.");

            RuleFor(p => p.ExpiracaoCartao)
                .NotEmpty()
                .WithMessage("O data de expração não foi informado.");

            RuleFor(p => p.CVVCartao)
                .Length(3 ,4)
                .WithMessage("O CVV não foi preenchido corretamente.");
        }
    }
}
