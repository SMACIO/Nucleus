﻿using System;
using Nucleus.EventBus;
using Nucleus.MultiTenancy;

namespace Nucleus.Domain.Entities.Events.Distributed
{
    [Serializable]
    [GenericEventName(Postfix = ".Created")]
    public class EntityCreatedEto<TEntityEto> : IEventDataMayHaveTenantId
    {
        public TEntityEto Entity { get; set; }

        public EntityCreatedEto(TEntityEto entity)
        {
            Entity = entity;
        }

        public virtual bool IsMultiTenant(out Guid? tenantId)
        {
            if (Entity is IMultiTenant multiTenantEntity)
            {
                tenantId = multiTenantEntity.TenantId;
                return true;
            }

            tenantId = null;
            return false;
        }
    }
}


