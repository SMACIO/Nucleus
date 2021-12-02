using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.Security;

namespace Nucleus.Settings
{
    [DependsOn(
        typeof(NucleusLocalizationAbstractionsModule),
        typeof(NucleusSecurityModule),
        typeof(NucleusMultiTenancyModule)
        )]
    public class NucleusSettingsModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusSettingOptions>(options =>
            {
                options.ValueProviders.Add<DefaultValueSettingValueProvider>();
                options.ValueProviders.Add<ConfigurationSettingValueProvider>();
                options.ValueProviders.Add<GlobalSettingValueProvider>();
                options.ValueProviders.Add<TenantSettingValueProvider>();
                options.ValueProviders.Add<UserSettingValueProvider>();
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(ISettingDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<NucleusSettingOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}






