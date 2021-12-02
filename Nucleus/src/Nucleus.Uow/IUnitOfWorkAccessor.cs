using JetBrains.Annotations;

namespace Nucleus.Uow
{
    public interface IUnitOfWorkAccessor
    {
        [CanBeNull]
        IUnitOfWork UnitOfWork { get; }

        void SetUnitOfWork([CanBeNull] IUnitOfWork unitOfWork);
    }
}
