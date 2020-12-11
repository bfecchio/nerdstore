using System;
using System.Text;
using Newtonsoft.Json;
using EventStore.ClientAPI;
using System.Threading.Tasks;
using System.Collections.Generic;

using NerdStore.Core.Messages;
using NerdStore.Core.Data.EventSourcing;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        #region Private Read-Only Fields

        private readonly IEventStoreService _eventStoreService;

        #endregion

        #region Constructors

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService ?? throw new ArgumentNullException(nameof(eventStoreService));
        }

        #endregion

        #region IEventSourcingRepository Members

        public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            await _eventStoreService.GetConnection()
                .AppendToStreamAsync(evento.AggregateId.ToString(), ExpectedVersion.Any, FormatarEvento(evento));
        }

        public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
        {
            var eventos = await _eventStoreService.GetConnection()
                .ReadStreamEventsForwardAsync(aggregateId.ToString(), start: 0, count: 500, resolveLinkTos: false);

            var listaEventos = new List<StoredEvent>();

            foreach (var resolvedEvent in eventos.Events)
            {
                var data = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var json = JsonConvert.DeserializeObject<BaseEvent>(data);

                var evento = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    json.Timestamp,
                    data
                );

                listaEventos.Add(evento);
            }

            return listaEventos;
        }

        #endregion

        #region Private Methods

        private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            yield return new EventData
            (
                Guid.NewGuid(),
                evento.MessageType,
                isJson: true,
                data: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
                metadata: new byte[] { }
            );
        }

        #endregion
    }
}
