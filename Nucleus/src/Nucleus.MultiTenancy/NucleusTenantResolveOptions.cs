using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.MultiTenancy
{
    public class NucleusTenantResolveOptions
    {
        [NotNull]
        public List<ITenantResolveContributor> TenantResolvers { get; }

        public NucleusTenantResolveOptions()
        {
            TenantResolvers = new List<ITenantResolveContributor>
            {
                new CurrentUserTenantResolveContributor()
            };
        }
    }
}


