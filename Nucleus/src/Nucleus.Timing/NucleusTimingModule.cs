using Nucleus.Localization;
using Nucleus.Localization.Resources.NucleusLocalization;
using Nucleus.Modularity;
using Nucleus.Settings;
using Nucleus.Timing.Localization.Resources.NucleusTiming;
using Nucleus.VirtualFileSystem;

namespace Nucleus.Timing
{
    [DependsOn(
        typeof(NucleusLocalizationModule),
        typeof(NucleusSettingsModule)
        )]
    public class NucleusTimingModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusTimingModule>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options
                    .Resources
                    .Add<NucleusTimingResource>("en")
                    .AddVirtualJson("/Nucleus/Timing/Localization");
            });
        }
    }
}







