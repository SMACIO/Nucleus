using Nucleus.Collections;

namespace Nucleus.Settings
{
    public class NucleusSettingOptions
    {
        public ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<ISettingValueProvider> ValueProviders { get; }

        public NucleusSettingOptions()
        {
            DefinitionProviders = new TypeList<ISettingDefinitionProvider>();
            ValueProviders = new TypeList<ISettingValueProvider>();
        }
    }
}




