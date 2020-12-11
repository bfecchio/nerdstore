using EventStore.ClientAPI;

namespace EventSourcing
{
    public interface IEventStoreService
    {
        #region IEventStoreService Members

        IEventStoreConnection GetConnection();

        #endregion
    }
}
