﻿using System;
using System.Collections.Generic;
using Nucleus.EventBus.Distributed;

namespace Nucleus.Domain.Entities.Events.Distributed
{
    public class EtoMappingDictionary : Dictionary<Type, EtoMappingDictionaryItem>
    {
        public void Add<TEntity, TEntityEto>(Type objectMappingContextType = null)
        {
            this[typeof(TEntity)] = new EtoMappingDictionaryItem(typeof(TEntityEto), objectMappingContextType);
        }
    }
}

