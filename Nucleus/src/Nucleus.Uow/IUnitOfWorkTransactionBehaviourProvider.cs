namespace Nucleus.Uow
{
    public interface IUnitOfWorkTransactionBehaviourProvider
    {
        bool? IsTransactional { get; }
    }
}
