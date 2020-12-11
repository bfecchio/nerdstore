using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using NerdStore.Core.Messages;

namespace NerdStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        #region IEventSourcingRepository Members

        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);

        #endregion
    }
}
