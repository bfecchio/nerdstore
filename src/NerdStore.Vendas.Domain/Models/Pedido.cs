using System;
using System.Linq;
using FluentValidation.Results;
using System.Collections.Generic;

using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enumerations;

namespace NerdStore.Vendas.Domain.Models
{
    public class Pedido : Entity, IAggregateRoot
    {
        #region Private Read-Only Fields

        private readonly List<PedidoItem> _pedidoItens;

        #endregion

        #region Public Properties

        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public IReadOnlyCollection<PedidoItem> PedidoItens => _pedidoItens;

        #endregion

        #region EF Properties
        
        public virtual Voucher Voucher { get; private set; }

        #endregion

        #region Constructors

        protected Pedido()
        {
            _pedidoItens = new List<PedidoItem>();
        }

        public Pedido(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal)
        {            
            ClienteId = clienteId;            
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;

            _pedidoItens = new List<PedidoItem>();
        }

        #endregion

        #region Behaviours

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidarSeAplicavel();
            if (validationResult.IsValid)
            {
                Voucher = voucher;
                VoucherUtilizado = true;
                CalcularValorPedido();
            }            

            return validationResult;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItens.Sum(x => x.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            var desconto = 0M;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem)
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            else
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }

            ValorTotal = (valor < 0 ? 0 : valor);
            Desconto = desconto;
        }

        public bool PedidoItemExistente(PedidoItem pedidoItem)
            => _pedidoItens.Any(x => x.ProdutoId == pedidoItem.ProdutoId);

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            if (!pedidoItem.EhValido()) return;

            pedidoItem.AssociarPedido(Id);

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);
                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);

                pedidoItem = itemExistente;

                _pedidoItens.Remove(itemExistente);
            }

            pedidoItem.CalcularValor();
            _pedidoItens.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            if (!pedidoItem.EhValido()) return;
            
            var itemExistente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);
            if (itemExistente == null)
                throw new DomainException("O item não pertence ao pedido.");

            _pedidoItens.Remove(itemExistente);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            if (!pedidoItem.EhValido()) return;

            pedidoItem.AssociarPedido(Id);

            var itemExistente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);
            if (itemExistente == null)
                throw new DomainException("O item não pertence ao pedido.");

            _pedidoItens.Remove(itemExistente);
            _pedidoItens.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem pedidoItem, int unidades)
        {
            pedidoItem.AtualizarUnidades(unidades);
            AtualizarItem(pedidoItem);
        }

        public void TornarRascunho()
            => PedidoStatus = PedidoStatus.Rascunho;

        public void IniciarPedido()
            => PedidoStatus = PedidoStatus.Iniciado;

        public void FinalizarPedido()
            => PedidoStatus = PedidoStatus.Pago;

        public void CancelarPedido()
            => PedidoStatus = PedidoStatus.Cancelado;

        #endregion

        #region Factory

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido { ClienteId = clienteId };                
                pedido.TornarRascunho();

                return pedido;
            }                
        }

        #endregion
    }
}