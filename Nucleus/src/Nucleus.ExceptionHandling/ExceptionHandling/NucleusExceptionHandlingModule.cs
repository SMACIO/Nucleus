using Nucleus.ExceptionHandling.Localization;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.VirtualFileSystem;

namespace Nucleus.ExceptionHandling
{
    /* TODO: This package is introduced in a later time by gathering
     * classes from multiple packages.
     * So, didn't change the original namespaces of the types to not introduce breaking changes.
     * We will re-design this package in a later time, probably with v5.0.
     */
    [DependsOn(
        typeof(NucleusLocalizationModule)
        )]
    public class NucleusExceptionHandlingModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusExceptionHandlingModule>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusExceptionHandlingResource>("en")
                    .AddVirtualJson("/Nucleus/ExceptionHandling/Localization");
            });
        }
    }
}







