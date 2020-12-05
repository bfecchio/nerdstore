using System;
using System.Collections.Generic;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain
{
    public class Voucher : Entity
    {
        #region Public Properties

        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        #endregion

        #region EF Properties

        public virtual ICollection<Pedido> Pedidos { get; private set; }

        #endregion

        #region Constructors

        protected Voucher()
        { }

        #endregion
    }
}
