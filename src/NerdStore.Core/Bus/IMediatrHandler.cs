using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    public interface IMediatrHandler
    {
        #region IMediatrHandler Members

        Task PublicarEvento<T>(T evento)
            where T : Event;

        #endregion
    }
}
