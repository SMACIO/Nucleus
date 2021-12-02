using Nucleus.AspNetCore.Authentication.OAuth;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;

namespace Nucleus.AspNetCore.Authentication.OpenIdConnect
{
    [DependsOn(
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusAspNetCoreAuthenticationOAuthModule))]
    public class NucleusAspNetCoreAuthenticationOpenIdConnectModule : NucleusModule
    {

    }
}





