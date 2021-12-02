using System.Security.Claims;
using System.Threading.Tasks;

namespace Nucleus.Security.Claims
{
    public interface INucleusClaimsPrincipalFactory
    {
        Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal existsClaimsPrincipal = null);
    }
}


