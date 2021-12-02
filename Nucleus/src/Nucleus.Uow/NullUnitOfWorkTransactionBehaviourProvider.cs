using Nucleus.DependencyInjection;

namespace Nucleus.Uow
{
    public class NullUnitOfWorkTransactionBehaviourProvider : IUnitOfWorkTransactionBehaviourProvider, ISingletonDependency
    {
        public bool? IsTransactional => null;
    }
}

