using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Nucleus.ExceptionHandling
{
    public interface IExceptionSubscriber
    {
        Task HandleAsync([NotNull] ExceptionNotificationContext context);
    }
}

