using Nucleus.Collections;

namespace Nucleus.EventBus.Distributed
{
    public class NucleusDistributedEventBusOptions
    {
        public ITypeList<IEventHandler> Handlers { get; }

        public OutboxConfigDictionary Outboxes { get; }

        public InboxConfigDictionary Inboxes { get; }
        public NucleusDistributedEventBusOptions()
        {
            Handlers = new TypeList<IEventHandler>();
            Outboxes = new OutboxConfigDictionary();
            Inboxes = new InboxConfigDictionary();
        }
    }
}




