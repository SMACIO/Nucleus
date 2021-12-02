using System;
using System.Collections.Generic;
using Nucleus.Domain.Entities;
using JetBrains.Annotations;

namespace Nucleus.Data
{
    public static class ConcurrencyStampExtensions
    {
        public static void SetConcurrencyStampIfNotNull(this IHasConcurrencyStamp entity, [CanBeNull] string concurrencyStamp)
        {
            if (!concurrencyStamp.IsNullOrEmpty())
            {
                entity.ConcurrencyStamp = concurrencyStamp;
            }
        }
    }
}


