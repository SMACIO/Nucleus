using Nucleus.Localization.Resources.NucleusLocalization;
using Nucleus.Modularity;
using Nucleus.Settings;
using Nucleus.VirtualFileSystem;

namespace Nucleus.Localization
{
    [DependsOn(
        typeof(NucleusVirtualFileSystemModule),
        typeof(NucleusSettingsModule),
        typeof(NucleusLocalizationAbstractionsModule)
        )]
    public class NucleusLocalizationModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            NucleusStringLocalizerFactory.Replace(context.Services);

            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusLocalizationModule>("Nucleus", "Nucleus");
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options
                    .Resources
                    .Add<DefaultResource>("en");

                options
                    .Resources
                    .Add<NucleusLocalizationResource>("en")
                    .AddVirtualJson("/Localization/Resources/NucleusLocalization");
            });
        }
    }
}











