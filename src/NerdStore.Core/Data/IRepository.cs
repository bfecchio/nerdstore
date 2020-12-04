using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Data
{
    public interface IRepository<T> : IDisposable
        where T : IAggregateRoot
    {
        #region IRepository Members

        IUnitOfWork UnitOfWork { get; }

        #endregion
    }
}
