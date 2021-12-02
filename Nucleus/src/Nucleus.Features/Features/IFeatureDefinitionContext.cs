using JetBrains.Annotations;
using Nucleus.Localization;

namespace Nucleus.Features
{
    public interface IFeatureDefinitionContext
    {
        FeatureGroupDefinition AddGroup([NotNull] string name, ILocalizableString displayName = null);

        FeatureGroupDefinition GetGroupOrNull(string name);
        
        void RemoveGroup(string name);
    }
}

