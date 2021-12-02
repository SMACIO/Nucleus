namespace Nucleus.Domain.Entities.Events.Distributed
{
    public class NucleusDistributedEntityEventOptions
    {
        public IAutoEntityDistributedEventSelectorList AutoEventSelectors { get; }
        
        public EtoMappingDictionary EtoMappings { get; set; }

        public NucleusDistributedEntityEventOptions()
        {
            AutoEventSelectors = new AutoEntityDistributedEventSelectorList();
            EtoMappings = new EtoMappingDictionary();
        }
    }
}


