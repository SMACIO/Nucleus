using JetBrains.Annotations;

namespace Nucleus.Tracing
{
    public interface ICorrelationIdProvider
    {
        [NotNull]
        string Get();
    }
}

