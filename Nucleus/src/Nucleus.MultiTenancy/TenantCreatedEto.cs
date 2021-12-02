using System;
using Nucleus.Domain.Entities.Events.Distributed;
using Nucleus.EventBus;

namespace Nucleus.MultiTenancy
{
    [Serializable]
    [EventName("nucleus.multi_tenancy.tenant.created")]
    public class TenantCreatedEto : EtoBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}



