using JetBrains.Annotations;

namespace Nucleus.Modularity
{
    public interface IOnPostApplicationInitialization
    {
        void OnPostApplicationInitialization([NotNull] ApplicationInitializationContext context);
    }
}
