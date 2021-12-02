using Nucleus.EventBus.Distributed;

namespace Nucleus.EventBus.Boxes
{
    public static class NucleusDistributedEventBusExtensions
    {
        public static ISupportsEventBoxes AsSupportsEventBoxes(this IDistributedEventBus eventBus)
        {
            var supportsEventBoxes = eventBus as ISupportsEventBoxes;
            if (supportsEventBoxes == null)
            {
                throw new NucleusException($"Given type ({eventBus.GetType().AssemblyQualifiedName}) should implement {nameof(ISupportsEventBoxes)}!");
            }
            
            return supportsEventBoxes;
        } 
    }
}



