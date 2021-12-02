using JetBrains.Annotations;

namespace Nucleus.Auditing
{
    public interface IAuditLogScope
    {
        [NotNull]
        AuditLogInfo Log { get; }
    }
}

