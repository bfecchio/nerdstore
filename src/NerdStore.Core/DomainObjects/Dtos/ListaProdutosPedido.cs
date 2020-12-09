using System;
using System.Collections.Generic;

namespace NerdStore.Core.DomainObjects.Dtos
{
    public class ListaProdutosPedido
    {
        #region Public Properties

        public Guid PedidoId { get; set; }
        public ICollection<Item> Itens { get; set; }

        #endregion

        #region Constructors
        
        public ListaProdutosPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
            Itens = new List<Item>();
        }

        #endregion
    }
}
