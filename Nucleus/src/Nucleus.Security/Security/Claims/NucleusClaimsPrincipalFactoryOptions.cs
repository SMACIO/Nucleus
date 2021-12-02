using Nucleus.Collections;

namespace Nucleus.Security.Claims
{
    public class NucleusClaimsPrincipalFactoryOptions
    {
        public ITypeList<INucleusClaimsPrincipalContributor> Contributors { get; }

        public NucleusClaimsPrincipalFactoryOptions()
        {
            Contributors = new TypeList<INucleusClaimsPrincipalContributor>();
        }
    }
}





