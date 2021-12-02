using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Nucleus.AspNetCore.Mvc.ApplicationConfigurations;
using Nucleus.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Nucleus.Caching;
using Nucleus.DependencyInjection;
using Nucleus.Threading;
using Nucleus.Users;

namespace Nucleus.AspNetCore.Mvc.Client
{
    [ExposeServices(
        typeof(MvcCachedApplicationConfigurationClient),
        typeof(ICachedApplicationConfigurationClient),
        typeof(IAsyncInitialize)
        )]
    public class MvcCachedApplicationConfigurationClient : ICachedApplicationConfigurationClient, ITransientDependency
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected NucleusApplicationConfigurationClientProxy ApplicationConfigurationAppService { get; }
        protected ICurrentUser CurrentUser { get; }
        protected IDistributedCache<ApplicationConfigurationDto> Cache { get; }

        public MvcCachedApplicationConfigurationClient(
            IDistributedCache<ApplicationConfigurationDto> cache,
            NucleusApplicationConfigurationClientProxy applicationConfigurationAppService,
            ICurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor)
        {
            ApplicationConfigurationAppService = applicationConfigurationAppService;
            CurrentUser = currentUser;
            HttpContextAccessor = httpContextAccessor;
            Cache = cache;
        }

        public async Task InitializeAsync()
        {
            await GetAsync();
        }

        public async Task<ApplicationConfigurationDto> GetAsync()
        {
            var cacheKey = CreateCacheKey();
            var httpContext = HttpContextAccessor?.HttpContext;

            if (httpContext != null && !httpContext.WebSockets.IsWebSocketRequest && httpContext.Items[cacheKey] is ApplicationConfigurationDto configuration)
            {

                return configuration;
            }


            configuration = await Cache.GetOrAddAsync(
                cacheKey,
                async () => await ApplicationConfigurationAppService.GetAsync(),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(300) //TODO: Should be configurable.
                }
            );

            if (httpContext != null && !httpContext.WebSockets.IsWebSocketRequest)
            {
                httpContext.Items[cacheKey] = configuration;
            }

            return configuration;
        }

        public ApplicationConfigurationDto Get()
        {
            var cacheKey = CreateCacheKey();
            var httpContext = HttpContextAccessor?.HttpContext;

            if (httpContext != null  && !httpContext.WebSockets.IsWebSocketRequest && httpContext.Items[cacheKey] is ApplicationConfigurationDto configuration)
            {
                return configuration;
            }

            return AsyncHelper.RunSync(GetAsync);
        }

        protected virtual string CreateCacheKey()
        {
            return MvcCachedApplicationConfigurationClientHelper.CreateCacheKey(CurrentUser);
        }
    }
}



