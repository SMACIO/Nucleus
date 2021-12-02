using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;

namespace Nucleus.AspNetCore.MultiTenancy
{
    [DependsOn(
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusAspNetCoreModule)
        )]
    public class NucleusAspNetCoreMultiTenancyModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Add(new QueryStringTenantResolveContributor());
                options.TenantResolvers.Add(new FormTenantResolveContributor());
                options.TenantResolvers.Add(new RouteTenantResolveContributor());
                options.TenantResolvers.Add(new HeaderTenantResolveContributor());
                options.TenantResolvers.Add(new CookieTenantResolveContributor());
            });
        }
    }
}






