﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Nucleus.DependencyInjection;
using Nucleus.EventBus.Distributed;
using Nucleus.EventBus.Local;
using Nucleus.Uow;

namespace Nucleus.EventBus
{
    [Dependency(ReplaceServices = true)]
    public class UnitOfWorkEventPublisher : IUnitOfWorkEventPublisher, ITransientDependency
    {
        private readonly ILocalEventBus _localEventBus;
        private readonly IDistributedEventBus _distributedEventBus;

        public UnitOfWorkEventPublisher(
            ILocalEventBus localEventBus,
            IDistributedEventBus distributedEventBus)
        {
            _localEventBus = localEventBus;
            _distributedEventBus = distributedEventBus;
        }
        
        public async Task PublishLocalEventsAsync(IEnumerable<UnitOfWorkEventRecord> localEvents)
        {
            foreach (var localEvent in localEvents)
            {
                await _localEventBus.PublishAsync(
                    localEvent.EventType,
                    localEvent.EventData,
                    onUnitOfWorkComplete: false
                );
            }
        }

        public async Task PublishDistributedEventsAsync(IEnumerable<UnitOfWorkEventRecord> distributedEvents)
        {
            foreach (var distributedEvent in distributedEvents)
            {
                await _distributedEventBus.PublishAsync(
                    distributedEvent.EventType,
                    distributedEvent.EventData,
                    onUnitOfWorkComplete: false,
                    useOutbox: distributedEvent.UseOutbox
                );
            }
        }
    }
}

