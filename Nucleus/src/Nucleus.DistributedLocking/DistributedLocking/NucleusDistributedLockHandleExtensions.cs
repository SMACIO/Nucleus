using System;
using Medallion.Threading;

namespace Nucleus.DistributedLocking
{
    public static class NucleusDistributedLockHandleExtensions
    {
        public static IDistributedSynchronizationHandle ToDistributedSynchronizationHandle(
            this INucleusDistributedLockHandle handle)
        {
            return handle.As<MedallionNucleusDistributedLockHandle>().Handle;
        }
    }
}



