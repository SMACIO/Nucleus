using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Authorization.Localization;
using Nucleus.Authorization.Permissions;
using Nucleus.Localization;
using Nucleus.Localization.ExceptionHandling;
using Nucleus.Modularity;
using Nucleus.Security;
using Nucleus.VirtualFileSystem;

namespace Nucleus.Authorization
{
    [DependsOn(
        typeof(NucleusAuthorizationAbstractionsModule),
        typeof(NucleusSecurityModule),
        typeof(NucleusLocalizationModule)
    )]
    public class NucleusAuthorizationModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(AuthorizationInterceptorRegistrar.RegisterIfNeeded);
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAuthorizationCore();

            context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
            context.Services.AddSingleton<IAuthorizationHandler, PermissionsRequirementHandler>();

            context.Services.TryAddTransient<DefaultAuthorizationPolicyProvider>();

            Configure<NucleusPermissionOptions>(options =>
            {
                options.ValueProviders.Add<UserPermissionValueProvider>();
                options.ValueProviders.Add<RolePermissionValueProvider>();
                options.ValueProviders.Add<ClientPermissionValueProvider>();
            });

            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusAuthorizationResource>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusAuthorizationResource>("en")
                    .AddVirtualJson("/Nucleus/Authorization/Localization");
            });

            Configure<NucleusExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Nucleus.Authorization", typeof(NucleusAuthorizationResource));
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IPermissionDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<NucleusPermissionOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}








