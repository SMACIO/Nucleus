using Nucleus.Localization.Resources.NucleusLocalization;
using Nucleus.Settings;

namespace Nucleus.Localization
{
    public class LocalizationSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                    "en", 
                    L("DisplayName:Nucleus.Localization.DefaultLanguage"),
                    L("Description:Nucleus.Localization.DefaultLanguage"),
                    isVisibleToClients: true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NucleusLocalizationResource>(name);
        }
    }
}



