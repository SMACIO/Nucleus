using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.Validation.Localization;
using Nucleus.VirtualFileSystem;

namespace Nucleus.Validation
{
    [DependsOn(
        typeof(NucleusValidationAbstractionsModule),
        typeof(NucleusLocalizationModule)
        )]
    public class NucleusValidationModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(ValidationInterceptorRegistrar.RegisterIfNeeded);
            AutoAddObjectValidationContributors(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusValidationResource>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusValidationResource>("en")
                    .AddVirtualJson("/Nucleus/Validation/Localization");
            });
        }

        private static void AutoAddObjectValidationContributors(IServiceCollection services)
        {
            var contributorTypes = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IObjectValidationContributor).IsAssignableFrom(context.ImplementationType))
                {
                    contributorTypes.Add(context.ImplementationType);
                }
            });

            services.Configure<NucleusValidationOptions>(options =>
            {
                options.ObjectValidationContributors.AddIfNotContains(contributorTypes);
            });
        }
    }
}







