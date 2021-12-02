using Nucleus.DependencyInjection;

namespace Nucleus.Settings
{
    public abstract class SettingDefinitionProvider : ISettingDefinitionProvider, ITransientDependency
    {
        public abstract void Define(ISettingDefinitionContext context);
    }
}

