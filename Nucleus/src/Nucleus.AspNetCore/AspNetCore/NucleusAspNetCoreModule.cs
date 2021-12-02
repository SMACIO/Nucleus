using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Nucleus.AspNetCore.Auditing;
using Nucleus.AspNetCore.VirtualFileSystem;
using Nucleus.Auditing;
using Nucleus.Authorization;
using Nucleus.ExceptionHandling;
using Nucleus.Http;
using Nucleus.Modularity;
using Nucleus.Security;
using Nucleus.Uow;
using Nucleus.Validation;
using Nucleus.VirtualFileSystem;

namespace Nucleus.AspNetCore
{
    [DependsOn(
        typeof(NucleusAuditingModule),
        typeof(NucleusSecurityModule),
        typeof(NucleusVirtualFileSystemModule),
        typeof(NucleusUnitOfWorkModule),
        typeof(NucleusHttpModule),
        typeof(NucleusAuthorizationModule),
        typeof(NucleusValidationModule),
        typeof(NucleusExceptionHandlingModule)
        )]
    public class NucleusAspNetCoreModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusAuditingOptions>(options =>
            {
                options.Contributors.Add(new AspNetCoreAuditLogContributor());
            });

            Configure<StaticFileOptions>(options =>
            {
                options.ContentTypeProvider = context.Services.GetRequiredService<NucleusFileExtensionContentTypeProvider>();
            });

            AddAspNetServices(context.Services);
            context.Services.AddObjectAccessor<IApplicationBuilder>();
            context.Services.AddNucleusDynamicOptions<RequestLocalizationOptions, NucleusRequestLocalizationOptionsManager>();
        }

        private static void AddAspNetServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var environment = context.GetEnvironmentOrNull();
            if (environment != null)
            {
                environment.WebRootFileProvider =
                    new CompositeFileProvider(
                        context.GetEnvironment().WebRootFileProvider,
                        context.ServiceProvider.GetRequiredService<IWebContentFileProvider>()
                    );
            }
        }
    }
}







