using Nucleus.Application.Localization.Resources.NucleusDdd;
using Nucleus.Auditing;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.VirtualFileSystem;

namespace Nucleus.Application
{
    [DependsOn(
        typeof(NucleusLocalizationModule),
        typeof(NucleusAuditingContractsModule)
        )]
    public class NucleusDddApplicationContractsModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusDddApplicationContractsModule>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusDddApplicationContractsResource>("en")
                    .AddVirtualJson("/Nucleus/Application/Localization/Resources/NucleusDdd");
            });
        }
    }
}







