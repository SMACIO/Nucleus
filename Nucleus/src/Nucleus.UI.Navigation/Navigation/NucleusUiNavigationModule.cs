using Nucleus.Authorization;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.UI.Navigation.Localization.Resource;
using Nucleus.VirtualFileSystem;

namespace Nucleus.UI.Navigation
{
    [DependsOn(typeof(NucleusUiModule), typeof(NucleusAuthorizationModule))]
    public class NucleusUiNavigationModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusUiNavigationModule>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusUiNavigationResource>("en")
                    .AddVirtualJson("/Nucleus/Ui/Navigation/Localization/Resource");
            });

            Configure<NucleusNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new DefaultMenuContributor());
            });
        }
    }
}







