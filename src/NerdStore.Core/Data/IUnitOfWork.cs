using System.Threading.Tasks;

namespace NerdStore.Core.Data
{
    public interface IUnitOfWork
    {
        #region IUnitOfWork Members

        Task<bool> Commit();

        #endregion
    }
}
