using System.Security.Claims;
using JetBrains.Annotations;
using Nucleus.MultiTenancy;

namespace System.Security.Principal
{
    public static class NucleusMultiTenancyClaimsIdentityExtensions
    {
        public static MultiTenancySides GetMultiTenancySide([NotNull] this IIdentity identity)
        {
            var tenantId = identity.FindTenantId();
            return tenantId.HasValue
                ? MultiTenancySides.Tenant
                : MultiTenancySides.Host;
        }

        public static MultiTenancySides GetMultiTenancySide([NotNull] this ClaimsPrincipal principal)
        {
            var tenantId = principal.FindTenantId();
            return tenantId.HasValue
                ? MultiTenancySides.Tenant
                : MultiTenancySides.Host;
        }
    }
}


