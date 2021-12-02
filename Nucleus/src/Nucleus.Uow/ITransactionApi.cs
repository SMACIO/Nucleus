using System;
using System.Threading.Tasks;

namespace Nucleus.Uow
{
    public interface ITransactionApi : IDisposable
    {
        Task CommitAsync();
    }
}
