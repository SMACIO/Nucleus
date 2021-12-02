using Nucleus.DependencyInjection;
using Nucleus.Threading;

namespace Nucleus.BackgroundWorkers
{
    /// <summary>
    /// Interface for a worker (thread) that runs on background to perform some tasks.
    /// </summary>
    public interface IBackgroundWorker : IRunnable, ISingletonDependency
    {

    }
}


