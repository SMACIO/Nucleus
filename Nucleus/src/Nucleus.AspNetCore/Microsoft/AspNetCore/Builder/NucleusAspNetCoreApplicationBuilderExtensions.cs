using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    public static class NucleusAspNetCoreApplicationBuilderExtensions
    {
        /// <summary>
        /// Maps endpoints configured with the <see cref="NucleusEndpointRouterOptions"/>.
        /// It internally uses the standard app.UseEndpoints(...) method.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="additionalConfigurationAction">Additional (and optional) endpoint configuration</param>
        /// <returns></returns>
        public static IApplicationBuilder UseConfiguredEndpoints(
            this IApplicationBuilder app,
            Action<IEndpointRouteBuilder> additionalConfigurationAction = null)
        {
            var options = app.ApplicationServices
                .GetRequiredService<IOptions<NucleusEndpointRouterOptions>>()
                .Value;

            if (!options.EndpointConfigureActions.Any())
            {
                return app;
            }

            return app.UseEndpoints(endpoints =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = new EndpointRouteBuilderContext(endpoints, scope.ServiceProvider);

                    foreach (var configureAction in options.EndpointConfigureActions)
                    {
                        configureAction(context);
                    }

                    additionalConfigurationAction?.Invoke(endpoints);
                }
            });
        }
    }
}



