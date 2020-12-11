using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        #region Private Read-Only Fields

        private readonly IEventStoreConnection _connection;

        #endregion

        #region Constructors

        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection.Create(configuration.GetConnectionString(nameof(EventStoreConnection)));
            _connection.ConnectAsync();            
        }

        #endregion

        #region IEventStoreService Members

        public IEventStoreConnection GetConnection()
            => _connection;

        #endregion
    }
}
