using System.Threading;
using System.Threading.Tasks;
using Nucleus.EventBus.Distributed;

namespace Nucleus.EventBus.Boxes
{
    public interface IInboxProcessor
    {
        Task StartAsync(InboxConfig inboxConfig, CancellationToken cancellationToken = default);
        
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}

