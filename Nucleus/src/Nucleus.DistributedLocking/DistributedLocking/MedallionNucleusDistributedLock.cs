using System;
using System.Threading;
using System.Threading.Tasks;
using Medallion.Threading;
using Nucleus.DependencyInjection;

namespace Nucleus.DistributedLocking
{
    [Dependency(ReplaceServices = true)]
    public class MedallionNucleusDistributedLock : INucleusDistributedLock, ITransientDependency
    {
        protected IDistributedLockProvider DistributedLockProvider { get; }

        public MedallionNucleusDistributedLock(IDistributedLockProvider distributedLockProvider)
        {
            DistributedLockProvider = distributedLockProvider;
        }
        
        public async Task<INucleusDistributedLockHandle> TryAcquireAsync(
            string name,
            TimeSpan timeout = default,
            CancellationToken cancellationToken = default)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var handle = await DistributedLockProvider.TryAcquireLockAsync(name, timeout, cancellationToken);
            if (handle == null)
            {
                return null;
            }

            return new MedallionNucleusDistributedLockHandle(handle);
        }
    }
}




