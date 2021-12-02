using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Nucleus.ExceptionHandling
{
    public interface IExceptionNotifier
    {
        Task NotifyAsync([NotNull] ExceptionNotificationContext context);
    }
}
