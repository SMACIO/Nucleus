using System.Collections.Generic;
using System.Threading.Tasks;
using Nucleus.DependencyInjection;

namespace Nucleus.Uow
{
    public class NullUnitOfWorkEventPublisher : IUnitOfWorkEventPublisher, ISingletonDependency
    {
        public Task PublishLocalEventsAsync(IEnumerable<UnitOfWorkEventRecord> localEvents)
        {
            return Task.CompletedTask;
        }

        public Task PublishDistributedEventsAsync(IEnumerable<UnitOfWorkEventRecord> distributedEvents)
        {
            return Task.CompletedTask;
        }
    }
}

