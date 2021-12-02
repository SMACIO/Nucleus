using System;

namespace Nucleus.EventBus
{
    public interface IEventNameProvider
    {
        string GetName(Type eventType);
    }
}
