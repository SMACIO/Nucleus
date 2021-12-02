using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Nucleus.Authorization;
using Nucleus.Features.Localization;
using Nucleus.Localization;
using Nucleus.Localization.ExceptionHandling;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.Validation;
using Nucleus.VirtualFileSystem;

namespace Nucleus.Features
{
    [DependsOn(
        typeof(NucleusLocalizationModule),
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusValidationModule),
        typeof(NucleusAuthorizationAbstractionsModule)
        )]
    public class NucleusFeaturesModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(FeatureInterceptorRegistrar.RegisterIfNeeded);
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.Configure<NucleusFeatureOptions>(options =>
            {
                options.ValueProviders.Add<DefaultValueFeatureValueProvider>();
                options.ValueProviders.Add<EditionFeatureValueProvider>();
                options.ValueProviders.Add<TenantFeatureValueProvider>();
            });

            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusFeatureResource>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusFeatureResource>("en")
                    .AddVirtualJson("/Nucleus/Features/Localization");
            });

            Configure<NucleusExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Nucleus.Feature", typeof(NucleusFeatureResource));
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IFeatureDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<NucleusFeatureOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}








