using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.DistributedLocking
{
    public class LocalNucleusDistributedLockHandle : INucleusDistributedLockHandle
    {
        private readonly SemaphoreSlim _semaphore;

        public LocalNucleusDistributedLockHandle(SemaphoreSlim semaphore)
        {
            _semaphore = semaphore;
        }

        public ValueTask DisposeAsync()
        {
            _semaphore.Release();
            return default;
        }
    }
}


