using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Nucleus.DependencyInjection;

namespace Nucleus.Authorization
{
    public interface INucleusAuthorizationService : IAuthorizationService, IServiceProviderAccessor
    {
        ClaimsPrincipal CurrentPrincipal { get; }
    }
}


