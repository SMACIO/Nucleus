using Nucleus.Collections;

namespace Nucleus.Features
{
    public class NucleusFeatureOptions
    {
        public ITypeList<IFeatureDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<IFeatureValueProvider> ValueProviders { get; }

        public NucleusFeatureOptions()
        {
            DefinitionProviders = new TypeList<IFeatureDefinitionProvider>();
            ValueProviders = new TypeList<IFeatureValueProvider>();
        }
    }
}




