namespace Nucleus.Modularity
{
    public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
    {
        public virtual void Initialize(ApplicationInitializationContext context, INucleusModule module)
        {
        }

        public virtual void Shutdown(ApplicationShutdownContext context, INucleusModule module)
        {
        }
    }
}

