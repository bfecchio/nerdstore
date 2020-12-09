using System;
using FluentValidation;

namespace NerdStore.Vendas.Domain.Models.Validators
{
    public class VoucherAplicavelValidator : AbstractValidator<Voucher>
    {
        #region Constructrors

        public VoucherAplicavelValidator()
        {
            RuleFor(p => p.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("Este voucher está expirado.");

            RuleFor(p => p.Ativo)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(p => p.Utilizado)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(p => p.Quantidade)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        #endregion

        #region Private Methods

        private bool DataVencimentoSuperiorAtual(DateTime dataValidade)
            => dataValidade >= DateTime.Now;

        #endregion
    }
}
