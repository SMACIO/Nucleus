using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Nucleus.BackgroundWorkers;
using Nucleus.DistributedLocking;
using Nucleus.EventBus.Abstractions;
using Nucleus.EventBus.Boxes;
using Nucleus.EventBus.Distributed;
using Nucleus.EventBus.Local;
using Nucleus.Guids;
using Nucleus.Json;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.Reflection;
using Nucleus.Threading;

namespace Nucleus.EventBus
{
    [DependsOn(
        typeof(NucleusEventBusAbstractionsModule),
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusJsonModule),
        typeof(NucleusGuidsModule),
        typeof(NucleusBackgroundWorkersModule),
        typeof(NucleusDistributedLockingAbstractionsModule)
        )]
    public class NucleusEventBusModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AddEventHandlers(context.Services);
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.AddBackgroundWorker<OutboxSenderManager>();
            context.AddBackgroundWorker<InboxProcessManager>();
        }

        private static void AddEventHandlers(IServiceCollection services)
        {
            var localHandlers = new List<Type>();
            var distributedHandlers = new List<Type>();

            services.OnRegistred(context =>
            {
                if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(ILocalEventHandler<>)))
                {
                    localHandlers.Add(context.ImplementationType);
                }
                else if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IDistributedEventHandler<>)))
                {
                    distributedHandlers.Add(context.ImplementationType);
                }
            });

            services.Configure<NucleusLocalEventBusOptions>(options =>
            {
                options.Handlers.AddIfNotContains(localHandlers);
            });

            services.Configure<NucleusDistributedEventBusOptions>(options =>
            {
                options.Handlers.AddIfNotContains(distributedHandlers);
            });
        }
    }
}






