using System.Threading.Tasks;
using Medallion.Threading;

namespace Nucleus.DistributedLocking
{
    public class MedallionNucleusDistributedLockHandle : INucleusDistributedLockHandle
    {
        public IDistributedSynchronizationHandle Handle { get; }

        public MedallionNucleusDistributedLockHandle(IDistributedSynchronizationHandle handle)
        {
            Handle = handle;
        }

        public ValueTask DisposeAsync()
        {
            return Handle.DisposeAsync();
        }
    }
}


