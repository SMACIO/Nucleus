using System;
using System.Threading.Tasks;

namespace Nucleus.Auditing
{
    public interface IAuditLogSaveHandle : IDisposable
    {
        Task SaveAsync();
    }
}
