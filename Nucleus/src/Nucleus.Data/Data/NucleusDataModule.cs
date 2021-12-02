using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.EventBus.Abstractions;
using Nucleus.Modularity;
using Nucleus.ObjectExtending;
using Nucleus.Uow;

namespace Nucleus.Data
{
    [DependsOn(
        typeof(NucleusObjectExtendingModule),
        typeof(NucleusUnitOfWorkModule),
        typeof(NucleusEventBusAbstractionsModule)
    )]
    public class NucleusDataModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDataSeedContributors(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<NucleusDbConnectionOptions>(configuration);

            context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusDbConnectionOptions>(options =>
            {
                options.Databases.RefreshIndexes();
            });
        }

        private static void AutoAddDataSeedContributors(IServiceCollection services)
        {
            var contributors = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IDataSeedContributor).IsAssignableFrom(context.ImplementationType))
                {
                    contributors.Add(context.ImplementationType);
                }
            });

            services.Configure<NucleusDataSeedOptions>(options =>
            {
                options.Contributors.AddIfNotContains(contributors);
            });
        }
    }
}






