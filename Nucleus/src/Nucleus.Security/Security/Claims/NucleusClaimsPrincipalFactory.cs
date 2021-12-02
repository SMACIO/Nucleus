using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Security.Claims
{
    public class NucleusClaimsPrincipalFactory : INucleusClaimsPrincipalFactory, ITransientDependency
    {
        public static string AuthenticationType => "Nucleus.Application";

        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected NucleusClaimsPrincipalFactoryOptions Options { get; }

        public NucleusClaimsPrincipalFactory(
            IServiceScopeFactory serviceScopeFactory,
            IOptions<NucleusClaimsPrincipalFactoryOptions> nucleusClaimOptions)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Options = nucleusClaimOptions.Value;
        }

        public virtual async Task<ClaimsPrincipal> CreateAsync(ClaimsPrincipal existsClaimsPrincipal = null)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var claimsPrincipal = existsClaimsPrincipal ?? new ClaimsPrincipal(new ClaimsIdentity(
                    AuthenticationType,
                    NucleusClaimTypes.UserName,
                    NucleusClaimTypes.Role));

                var context = new NucleusClaimsPrincipalContributorContext(claimsPrincipal, scope.ServiceProvider);

                foreach (var contributorType in Options.Contributors)
                {
                    var contributor = (INucleusClaimsPrincipalContributor) scope.ServiceProvider.GetRequiredService(contributorType);
                    await contributor.ContributeAsync(context);
                }

                return claimsPrincipal;
            }
        }
    }
}









