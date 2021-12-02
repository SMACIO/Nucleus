using System;
using Microsoft.AspNetCore.Http;
using Nucleus.DependencyInjection;

namespace Nucleus.AspNetCore.DependencyInjection
{
    public class HttpContextClientScopeServiceProviderAccessor :
        IClientScopeServiceProviderAccessor,
        ISingletonDependency
    {
        public IServiceProvider ServiceProvider
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    throw new NucleusException("HttpContextClientScopeServiceProviderAccessor should only be used in a web request scope!");
                }

                return httpContext.RequestServices;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextClientScopeServiceProviderAccessor(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}



