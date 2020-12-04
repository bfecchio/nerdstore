using System;
using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects
{
    public class DomainEvent : Event
    {
        #region Constructors

        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        #endregion
    }
}
