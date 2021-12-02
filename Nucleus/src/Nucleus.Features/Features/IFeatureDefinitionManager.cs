using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.Features
{
    public interface IFeatureDefinitionManager
    {
        [NotNull]
        FeatureDefinition Get([NotNull] string name);

        IReadOnlyList<FeatureDefinition> GetAll();

        FeatureDefinition GetOrNull(string name);

        IReadOnlyList<FeatureGroupDefinition> GetGroups();
    }
}

