using Nucleus.Localization;
using Nucleus.Settings;
using Nucleus.Timing.Localization.Resources.NucleusTiming;

namespace Nucleus.Timing
{
    public class TimingSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(TimingSettingNames.TimeZone,
                    "UTC",
                    L("DisplayName:Nucleus.Timing.Timezone"),
                    L("Description:Nucleus.Timing.Timezone"),
                    isVisibleToClients: true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NucleusTimingResource>(name);
        }
    }
}




