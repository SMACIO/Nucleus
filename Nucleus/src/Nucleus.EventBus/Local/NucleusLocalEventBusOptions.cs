using Nucleus.Collections;

namespace Nucleus.EventBus.Local
{
    public class NucleusLocalEventBusOptions
    {
        public ITypeList<IEventHandler> Handlers { get; }

        public NucleusLocalEventBusOptions()
        {
            Handlers = new TypeList<IEventHandler>();
        }
    }
}



