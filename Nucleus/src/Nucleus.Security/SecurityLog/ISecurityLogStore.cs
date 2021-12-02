using System.Threading.Tasks;

namespace Nucleus.SecurityLog
{
    public interface ISecurityLogStore
    {
        Task SaveAsync(SecurityLogInfo securityLogInfo);
    }
}

