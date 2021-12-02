using System;
using System.Security.Claims;
using JetBrains.Annotations;

namespace Nucleus.Security.Claims
{
    public class NucleusClaimsPrincipalContributorContext
    {
        [NotNull]
        public ClaimsPrincipal ClaimsPrincipal { get; }

        [NotNull]
        public IServiceProvider ServiceProvider { get; }

        public NucleusClaimsPrincipalContributorContext(
            [NotNull] ClaimsPrincipal claimsIdentity,
            [NotNull] IServiceProvider serviceProvider)
        {
            ClaimsPrincipal = claimsIdentity;
            ServiceProvider = serviceProvider;
        }
    }
}



