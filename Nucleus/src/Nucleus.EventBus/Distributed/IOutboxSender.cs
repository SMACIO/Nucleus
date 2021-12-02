using System.Threading;
using System.Threading.Tasks;
using Nucleus.EventBus.Distributed;

namespace Nucleus.EventBus.Boxes
{
    public interface IOutboxSender
    {
        Task StartAsync(OutboxConfig outboxConfig, CancellationToken cancellationToken = default);
        
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}

