using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Nucleus.ApiVersioning;
using Nucleus.Application;
using Nucleus.AspNetCore.Mvc.AntiForgery;
using Nucleus.AspNetCore.Mvc.ApiExploring;
using Nucleus.AspNetCore.Mvc.Conventions;
using Nucleus.AspNetCore.Mvc.DataAnnotations;
using Nucleus.AspNetCore.Mvc.DependencyInjection;
using Nucleus.AspNetCore.Mvc.Json;
using Nucleus.AspNetCore.Mvc.Localization;
using Nucleus.AspNetCore.VirtualFileSystem;
using Nucleus.DependencyInjection;
using Nucleus.Http;
using Nucleus.DynamicProxy;
using Nucleus.GlobalFeatures;
using Nucleus.Http.Modeling;
using Nucleus.Json;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.UI;
using Nucleus.UI.Navigation;

namespace Nucleus.AspNetCore.Mvc
{
    [DependsOn(
        typeof(NucleusAspNetCoreModule),
        typeof(NucleusLocalizationModule),
        typeof(NucleusApiVersioningAbstractionsModule),
        typeof(NucleusAspNetCoreMvcContractsModule),
        typeof(NucleusUiNavigationModule),
        typeof(NucleusGlobalFeaturesModule),
        typeof(NucleusDddApplicationModule)
        )]
    public class NucleusAspNetCoreMvcModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            DynamicProxyIgnoreTypes.Add<ControllerBase>();
            DynamicProxyIgnoreTypes.Add<PageModel>();
            DynamicProxyIgnoreTypes.Add<ViewComponent>();

            context.Services.AddConventionalRegistrar(new NucleusAspNetCoreMvcConventionalRegistrar());
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusApiDescriptionModelOptions>(options =>
            {
                options.IgnoredInterfaces.AddIfNotContains(typeof(IAsyncActionFilter));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IFilterMetadata));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IActionFilter));
            });

            Configure<NucleusRemoteServiceApiDescriptionProviderOptions>(options =>
            {
                var statusCodes = new List<int>
                {
                    (int) HttpStatusCode.Forbidden,
                    (int) HttpStatusCode.Unauthorized,
                    (int) HttpStatusCode.BadRequest,
                    (int) HttpStatusCode.NotFound,
                    (int) HttpStatusCode.NotImplemented,
                    (int) HttpStatusCode.InternalServerError
                };

                options.SupportedResponseTypes.AddIfNotContains(statusCodes.Select(statusCode => new ApiResponseType
                {
                    Type = typeof(RemoteServiceErrorResponse),
                    StatusCode = statusCode
                }));
            });

            context.Services.PostConfigure<NucleusAspNetCoreMvcOptions>(options =>
            {
                if (options.MinifyGeneratedScript == null)
                {
                    options.MinifyGeneratedScript = context.Services.GetHostingEnvironment().IsProduction();
                }
            });

            var mvcCoreBuilder = context.Services.AddMvcCore(options =>
            {
                options.Filters.Add(new NucleusAutoValidateAntiforgeryTokenAttribute());
            });
            context.Services.ExecutePreConfiguredActions(mvcCoreBuilder);

            var nucleusMvcDataAnnotationsLocalizationOptions = context.Services
                .ExecutePreConfiguredActions(
                    new NucleusMvcDataAnnotationsLocalizationOptions()
                );

            context.Services
                .AddSingleton<IOptions<NucleusMvcDataAnnotationsLocalizationOptions>>(
                    new OptionsWrapper<NucleusMvcDataAnnotationsLocalizationOptions>(
                        nucleusMvcDataAnnotationsLocalizationOptions
                    )
                );

            var mvcBuilder = context.Services.AddMvc()
                .AddRazorRuntimeCompilation()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var resourceType = nucleusMvcDataAnnotationsLocalizationOptions
                            .AssemblyResources
                            .GetOrDefault(type.Assembly);

                        if (resourceType != null)
                        {
                            return factory.Create(resourceType);
                        }

                        return factory.CreateDefaultOrNull() ??
                               factory.Create(type);
                    };
                })
                .AddViewLocalization(); //TODO: How to configure from the application? Also, consider to move to a UI module since APIs does not care about it.

            mvcCoreBuilder.AddNucleusHybridJson();

            Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(
                    new RazorViewEngineVirtualFileProvider(
                        context.Services.GetSingletonInstance<IObjectAccessor<IServiceProvider>>()
                    )
                );
            });

            context.Services.ExecutePreConfiguredActions(mvcBuilder);

            //TODO: AddViewLocalization by default..?

            context.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Use DI to create controllers
            mvcBuilder.AddControllersAsServices();

            //Use DI to create view components
            mvcBuilder.AddViewComponentsAsServices();

            //Use DI to create razor page
            context.Services.Replace(ServiceDescriptor.Singleton<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider>());

            //Add feature providers
            var partManager = context.Services.GetSingletonInstance<ApplicationPartManager>();
            var application = context.Services.GetSingletonInstance<INucleusApplication>();

            partManager.FeatureProviders.Add(new NucleusConventionalControllerFeatureProvider(application));
            partManager.ApplicationParts.AddIfNotContains(typeof(NucleusAspNetCoreMvcModule).Assembly);

            context.Services.Replace(ServiceDescriptor.Singleton<IValidationAttributeAdapterProvider, NucleusValidationAttributeAdapterProvider>());
            context.Services.AddSingleton<ValidationAttributeAdapterProvider>();

            Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddNucleus(context.Services);
            });

            Configure<NucleusEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(endpointContext =>
                {
                    endpointContext.Endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                    endpointContext.Endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpointContext.Endpoints.MapRazorPages();
                });
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            ApplicationPartSorter.Sort(
                context.Services.GetSingletonInstance<ApplicationPartManager>(),
                context.Services.GetSingletonInstance<IModuleContainer>()
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AddApplicationParts(context);
        }

        private static void AddApplicationParts(ApplicationInitializationContext context)
        {
            var partManager = context.ServiceProvider.GetService<ApplicationPartManager>();
            if (partManager == null)
            {
                return;
            }

            //Plugin modules
            var moduleAssemblies = context
                .ServiceProvider
                .GetRequiredService<IModuleContainer>()
                .Modules
                .Where(m => m.IsLoadedAsPlugIn)
                .Select(m => m.Type.Assembly)
                .Distinct();

            AddToApplicationParts(partManager, moduleAssemblies);

            //Controllers for application services
            var controllerAssemblies = context
                .ServiceProvider
                .GetRequiredService<IOptions<NucleusAspNetCoreMvcOptions>>()
                .Value
                .ConventionalControllers
                .ConventionalControllerSettings
                .Select(s => s.Assembly)
                .Distinct();

            AddToApplicationParts(partManager, controllerAssemblies);
        }

        private static void AddToApplicationParts(ApplicationPartManager partManager, IEnumerable<Assembly> moduleAssemblies)
        {
            foreach (var moduleAssembly in moduleAssemblies)
            {
                partManager.ApplicationParts.AddIfNotContains(moduleAssembly);
            }
        }
    }
}










