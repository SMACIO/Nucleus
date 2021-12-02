using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Microsoft.AspNetCore.RequestLocalization
{
    public class NucleusRequestLocalizationMiddleware : IMiddleware, ITransientDependency
    {
        public const string HttpContextItemName = "__NucleusSetCultureCookie";

        private readonly INucleusRequestLocalizationOptionsProvider _requestLocalizationOptionsProvider;
        private readonly ILoggerFactory _loggerFactory;

        public NucleusRequestLocalizationMiddleware(
            INucleusRequestLocalizationOptionsProvider requestLocalizationOptionsProvider,
            ILoggerFactory loggerFactory)
        {
            _requestLocalizationOptionsProvider = requestLocalizationOptionsProvider;
            _loggerFactory = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var middleware = new RequestLocalizationMiddleware(
                next,
                new OptionsWrapper<RequestLocalizationOptions>(await _requestLocalizationOptionsProvider.GetLocalizationOptionsAsync()),
                _loggerFactory
            );

            context.Response.OnStarting(() =>
            {
                if (context.Items[HttpContextItemName] == null)
                {
                    var requestCultureFeature = context.Features.Get<IRequestCultureFeature>();
                    if (requestCultureFeature?.Provider is QueryStringRequestCultureProvider)
                    {
                        NucleusRequestCultureCookieHelper.SetCultureCookie(
                            context,
                            requestCultureFeature.RequestCulture
                        );
                    }
                }

                return Task.CompletedTask;
            });

            await middleware.Invoke(context);
        }
    }
}





