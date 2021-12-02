using JetBrains.Annotations;
using Nucleus.DependencyInjection;

namespace Nucleus.Modularity
{
    public interface IModuleLifecycleContributor : ITransientDependency
    {
        void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] INucleusModule module);

        void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] INucleusModule module);
    }
}



