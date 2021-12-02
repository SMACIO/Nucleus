using Localization.Resources.NucleusUi;
using Nucleus.ExceptionHandling;
using Nucleus.ExceptionHandling.Localization;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.VirtualFileSystem;

namespace Nucleus.UI
{
    [DependsOn(
        typeof(NucleusExceptionHandlingModule)
    )]
    public class NucleusUiModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusUiModule>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusUiResource>("en")
                    .AddBaseTypes(typeof(NucleusExceptionHandlingResource))
                    .AddVirtualJson("/Localization/Resources/NucleusUi");
            });
        }
    }
}






