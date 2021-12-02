using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Nucleus.ApiVersioning;
using Nucleus.AspNetCore.Mvc;
using Nucleus.AspNetCore.Mvc.Conventions;
using Nucleus.AspNetCore.Mvc.Versioning;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NucleusApiVersioningExtensions
    {
        public static IServiceCollection AddNucleusApiVersioning(this IServiceCollection services, Action<ApiVersioningOptions> setupAction)
        {
            services.AddTransient<IRequestedApiVersion, HttpContextRequestedApiVersion>();
            services.AddTransient<IApiControllerSpecification, NucleusConventionalApiControllerSpecification>();

            services.AddApiVersioning(setupAction);

            return services;
        }

        public static void ConfigureNucleus(this ApiVersioningOptions options, NucleusAspNetCoreMvcOptions mvcOptions)
        {
            foreach (var setting in mvcOptions.ConventionalControllers.ConventionalControllerSettings)
            {
                if (setting.ApiVersionConfigurer == null)
                {
                    ConfigureApiVersionsByConvention(options, setting);
                }
                else
                {
                    setting.ApiVersionConfigurer.Invoke(options);
                }
            }
        }

        private static void ConfigureApiVersionsByConvention(ApiVersioningOptions options, ConventionalControllerSetting setting)
        {
            foreach (var controllerType in setting.ControllerTypes)
            {
                var controllerBuilder = options.Conventions.Controller(controllerType);

                if (setting.ApiVersions.Any())
                {
                    foreach (var apiVersion in setting.ApiVersions)
                    {
                        controllerBuilder.HasApiVersion(apiVersion);
                    }
                }
                else
                {
                    if (!controllerType.IsDefined(typeof(ApiVersionAttribute), true))
                    {
                        controllerBuilder.IsApiVersionNeutral();
                    }
                }
            }
        }
    }
}




