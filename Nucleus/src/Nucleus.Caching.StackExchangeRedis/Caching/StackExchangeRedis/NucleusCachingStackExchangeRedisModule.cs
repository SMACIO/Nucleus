using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Modularity;

namespace Nucleus.Caching.StackExchangeRedis
{
    [DependsOn(
        typeof(NucleusCachingModule)
        )]
    public class NucleusCachingStackExchangeRedisModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var redisEnabled = configuration["Redis:IsEnabled"];
            if (redisEnabled.IsNullOrEmpty() || bool.Parse(redisEnabled))
            {
                context.Services.AddStackExchangeRedisCache(options =>
                {
                    var redisConfiguration = configuration["Redis:Configuration"];
                    if (!redisConfiguration.IsNullOrEmpty())
                    {
                        options.Configuration = redisConfiguration;
                    }
                });

                context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, NucleusRedisCache>());
            }
        }
    }
}





