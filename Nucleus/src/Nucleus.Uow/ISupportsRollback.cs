using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.Uow
{
    public interface ISupportsRollback
    {
        Task RollbackAsync(CancellationToken cancellationToken);
    }
}

