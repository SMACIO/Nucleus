using System.Collections.Generic;
using Nucleus.AspNetCore.MultiTenancy;

namespace Nucleus.MultiTenancy
{
    public static class NucleusMultiTenancyOptionsExtensions
    {
        public static void AddDomainTenantResolver(this NucleusTenantResolveOptions options, string domainFormat)
        {
            options.TenantResolvers.InsertAfter(
                r => r is CurrentUserTenantResolveContributor,
                new DomainTenantResolveContributor(domainFormat)
            );
        }
    }
}



