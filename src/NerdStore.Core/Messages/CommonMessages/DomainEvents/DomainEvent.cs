using System;

namespace NerdStore.Core.Messages.CommonMessages.DomainEvents
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
