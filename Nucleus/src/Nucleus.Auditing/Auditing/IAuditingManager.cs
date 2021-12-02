using JetBrains.Annotations;

namespace Nucleus.Auditing
{
    public interface IAuditingManager
    {
        [CanBeNull]
        IAuditLogScope Current { get; }

        IAuditLogSaveHandle BeginScope();
    }
}
