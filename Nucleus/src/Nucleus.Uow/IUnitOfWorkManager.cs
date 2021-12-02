using JetBrains.Annotations;

namespace Nucleus.Uow
{
    public interface IUnitOfWorkManager
    {
        [CanBeNull]
        IUnitOfWork Current { get; }

        [NotNull]
        IUnitOfWork Begin([NotNull] NucleusUnitOfWorkOptions options, bool requiresNew = false);

        [NotNull]
        IUnitOfWork Reserve([NotNull] string reservationName, bool requiresNew = false);

        void BeginReserved([NotNull] string reservationName, [NotNull] NucleusUnitOfWorkOptions options);

        bool TryBeginReserved([NotNull] string reservationName, [NotNull] NucleusUnitOfWorkOptions options);
    }
}

