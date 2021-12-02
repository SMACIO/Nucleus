using System;
using System.Threading.Tasks;

namespace Nucleus.SecurityLog
{
    public interface ISecurityLogManager
    {
        Task SaveAsync(Action<SecurityLogInfo> saveAction = null);
    }
}

