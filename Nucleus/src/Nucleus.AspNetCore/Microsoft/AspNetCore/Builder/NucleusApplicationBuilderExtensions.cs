using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nucleus;
using Nucleus.AspNetCore.Auditing;
using Nucleus.AspNetCore.ExceptionHandling;
using Nucleus.AspNetCore.Security;
using Nucleus.AspNetCore.Security.Claims;
using Nucleus.AspNetCore.Tracing;
using Nucleus.AspNetCore.Uow;
using Nucleus.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class NucleusApplicationBuilderExtensions
    {
        private const string ExceptionHandlingMiddlewareMarker = "_NucleusExceptionHandlingMiddleware_Added";

        public static void InitializeApplication([NotNull] this IApplicationBuilder app)
        {
            Check.NotNull(app, nameof(app));

            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
            var application = app.ApplicationServices.GetRequiredService<INucleusApplicationWithExternalServiceProvider>();
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                application.Shutdown();
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                application.Dispose();
            });

            application.Initialize(app.ApplicationServices);
        }

        public static IApplicationBuilder UseAuditing(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<NucleusAuditingMiddleware>();
        }

        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
        {
            return app
                .UseNucleusExceptionHandling()
                .UseMiddleware<NucleusUnitOfWorkMiddleware>();
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<NucleusCorrelationIdMiddleware>();
        }

        public static IApplicationBuilder UseNucleusRequestLocalization(this IApplicationBuilder app,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            app.ApplicationServices
                .GetRequiredService<INucleusRequestLocalizationOptionsProvider>()
                .InitLocalizationOptions(optionsAction);

            return app.UseMiddleware<NucleusRequestLocalizationMiddleware>();
        }

        public static IApplicationBuilder UseNucleusExceptionHandling(this IApplicationBuilder app)
        {
            if (app.Properties.ContainsKey(ExceptionHandlingMiddlewareMarker))
            {
                return app;
            }

            app.Properties[ExceptionHandlingMiddlewareMarker] = true;
            return app.UseMiddleware<NucleusExceptionHandlingMiddleware>();
        }

        public static IApplicationBuilder UseNucleusClaimsMap(this IApplicationBuilder app)
        {
            return app.UseMiddleware<NucleusClaimsMapMiddleware>();
        }

        public static IApplicationBuilder UseNucleusSecurityHeaders(this IApplicationBuilder app)
        {
            return app.UseMiddleware<NucleusSecurityHeadersMiddleware>();
        }
    }
}





