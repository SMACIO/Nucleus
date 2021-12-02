using System;
using Nucleus.Domain.Entities.Events.Distributed;
using Nucleus.EventBus;

namespace Nucleus.Data
{
    [Serializable]
    [EventName("nucleus.data.apply_database_migrations")]
    public class ApplyDatabaseMigrationsEto : EtoBase
    {
        public Guid? TenantId { get; set; }
        
        public string DatabaseName { get; set; }
    }
}


