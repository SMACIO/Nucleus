using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Nucleus.DependencyInjection;
using Nucleus.Tracing;

namespace Nucleus.AspNetCore.Tracing
{
    public class NucleusCorrelationIdMiddleware : IMiddleware, ITransientDependency
    {
        private readonly NucleusCorrelationIdOptions _options;
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public NucleusCorrelationIdMiddleware(IOptions<NucleusCorrelationIdOptions> options,
            ICorrelationIdProvider correlationIdProvider)
        {
            _options = options.Value;
            _correlationIdProvider = correlationIdProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var correlationId = _correlationIdProvider.Get();

            try
            {
                await next(context);
            }
            finally
            {
                CheckAndSetCorrelationIdOnResponse(context, _options, correlationId);
            }
        }

        protected virtual void CheckAndSetCorrelationIdOnResponse(
            HttpContext httpContext,
            NucleusCorrelationIdOptions options,
            string correlationId)
        {
            if (httpContext.Response.HasStarted)
            {
                return;
            }

            if (!options.SetResponseHeader)
            {
                return;
            }

            if (httpContext.Response.Headers.ContainsKey(options.HttpHeaderName))
            {
                return;
            }

            httpContext.Response.Headers[options.HttpHeaderName] = correlationId;
        }
    }
}





