using System.Threading.Tasks;

namespace Nucleus.Security.Claims
{
    public interface INucleusClaimsPrincipalContributor
    {
        Task ContributeAsync(NucleusClaimsPrincipalContributorContext context);
    }
}



