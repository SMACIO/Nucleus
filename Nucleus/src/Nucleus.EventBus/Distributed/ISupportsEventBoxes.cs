using System.Threading.Tasks;

namespace Nucleus.EventBus.Distributed
{
    public interface ISupportsEventBoxes
    {
        Task PublishFromOutboxAsync(
            OutgoingEventInfo outgoingEvent,
            OutboxConfig outboxConfig
        );

        Task ProcessFromInboxAsync(
            IncomingEventInfo incomingEvent,
            InboxConfig inboxConfig 
        );
    }
}
