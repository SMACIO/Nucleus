using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Tracing;

namespace Nucleus.AspNetCore.Tracing
{
    [Dependency(ReplaceServices = true)]
    public class AspNetCoreCorrelationIdProvider : ICorrelationIdProvider, ITransientDependency
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected NucleusCorrelationIdOptions Options { get; }

        public AspNetCoreCorrelationIdProvider(
            IHttpContextAccessor httpContextAccessor,
            IOptions<NucleusCorrelationIdOptions> options)
        {
            HttpContextAccessor = httpContextAccessor;
            Options = options.Value;
        }

        public virtual string Get()
        {
            if (HttpContextAccessor.HttpContext?.Request?.Headers == null)
            {
                return CreateNewCorrelationId();
            }

            string correlationId = HttpContextAccessor.HttpContext.Request.Headers[Options.HttpHeaderName];

            if (correlationId.IsNullOrEmpty())
            {
                lock (HttpContextAccessor.HttpContext.Request.Headers)
                {
                    if (correlationId.IsNullOrEmpty())
                    {
                        correlationId = CreateNewCorrelationId();
                        HttpContextAccessor.HttpContext.Request.Headers[Options.HttpHeaderName] = correlationId;
                    }
                }
            }

            return correlationId;
        }

        protected virtual string CreateNewCorrelationId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}




