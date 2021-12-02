using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.Uow
{
    public interface ISupportsSavingChanges
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
