using JetBrains.Annotations;

namespace Nucleus.Domain.Entities.Events.Distributed
{
    public interface IEntityToEtoMapper
    {
        [CanBeNull]
        object Map(object entityObj);
    }
}

