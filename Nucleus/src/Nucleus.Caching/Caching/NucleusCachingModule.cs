using Microsoft.Extensions.DependencyInjection;
using System;
using Nucleus.Json;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.Serialization;
using Nucleus.Threading;
using Nucleus.Uow;

namespace Nucleus.Caching
{
    [DependsOn(
        typeof(NucleusThreadingModule),
        typeof(NucleusSerializationModule),
        typeof(NucleusUnitOfWorkModule),
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusJsonModule))]
    public class NucleusCachingModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMemoryCache();
            context.Services.AddDistributedMemoryCache();

            context.Services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            context.Services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

            context.Services.Configure<NucleusDistributedCacheOptions>(cacheOptions =>
            {
                cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
            });
        }
    }
}






