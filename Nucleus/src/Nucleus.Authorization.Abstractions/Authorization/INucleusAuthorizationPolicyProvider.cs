using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Nucleus.Authorization
{
    public interface INucleusAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        Task<List<string>> GetPoliciesNamesAsync();
    }
}

