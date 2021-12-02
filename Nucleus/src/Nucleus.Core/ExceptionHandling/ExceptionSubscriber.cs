using System.Threading.Tasks;
using Nucleus.DependencyInjection;

namespace Nucleus.ExceptionHandling
{
    [ExposeServices(typeof(IExceptionSubscriber))]
    public abstract class ExceptionSubscriber : IExceptionSubscriber, ITransientDependency
    {
        public abstract Task HandleAsync(ExceptionNotificationContext context);
    }
}

