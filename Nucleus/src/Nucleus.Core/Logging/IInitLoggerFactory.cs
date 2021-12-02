namespace Nucleus.Logging
{
    public interface IInitLoggerFactory
    {
        IInitLogger<T> Create<T>();
    }
}

