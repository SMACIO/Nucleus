namespace Nucleus.Uow
{
    public interface IAmbientUnitOfWork : IUnitOfWorkAccessor
    {
        IUnitOfWork GetCurrentByChecking();
    }
}
