using System;
using System.Collections.Generic;

namespace Nucleus.Domain.Entities.Events.Distributed
{
    public interface IAutoEntityDistributedEventSelectorList : IList<NamedTypeSelector>
    {
    }
}
