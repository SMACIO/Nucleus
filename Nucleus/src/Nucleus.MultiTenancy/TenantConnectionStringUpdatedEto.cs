using System;
using Nucleus.Domain.Entities.Events.Distributed;
using Nucleus.EventBus;

namespace Nucleus.MultiTenancy
{
    [Serializable]
    [EventName("nucleus.multi_tenancy.tenant.connection_string.updated")]
    public class TenantConnectionStringUpdatedEto : EtoBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ConnectionStringName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}



