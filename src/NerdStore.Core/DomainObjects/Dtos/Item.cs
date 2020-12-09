using System;

namespace NerdStore.Core.DomainObjects.Dtos
{
    public class Item
    {
        #region Public Properties

        public Guid Id { get; set; }
        public int Quantidade { get; set; }

        #endregion

        #region Constructors

        public Item()
        { }

        public Item(Guid id, int quantidade)
        {
            Id = id;
            Quantidade = quantidade;
        }

        #endregion
    }
}
