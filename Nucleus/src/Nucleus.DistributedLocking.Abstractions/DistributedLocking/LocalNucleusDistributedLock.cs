using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Nucleus.DependencyInjection;

namespace Nucleus.DistributedLocking
{
    public class LocalNucleusDistributedLock : INucleusDistributedLock, ISingletonDependency
    {
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _localSyncObjects = new();

        public async Task<INucleusDistributedLockHandle> TryAcquireAsync(
            string name, 
            TimeSpan timeout = default,
            CancellationToken cancellationToken = default)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            
            var semaphore = _localSyncObjects.GetOrAdd(name, _ => new SemaphoreSlim(1, 1));

            if (!await semaphore.WaitAsync(timeout, cancellationToken))
            {
                return null;
            }

            return new LocalNucleusDistributedLockHandle(semaphore);
        }
    }
}




