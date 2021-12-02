using System.Threading.Tasks;

namespace Nucleus.Auditing
{
    public interface IAuditingStore
    {
        Task SaveAsync(AuditLogInfo auditInfo);
    }
}
