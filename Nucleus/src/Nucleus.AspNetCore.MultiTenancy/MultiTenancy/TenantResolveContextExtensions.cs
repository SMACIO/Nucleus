using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.MultiTenancy;

namespace Nucleus.AspNetCore.MultiTenancy
{
    public static class TenantResolveContextExtensions
    {
        public static NucleusAspNetCoreMultiTenancyOptions GetNucleusAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
        {
            return context.ServiceProvider.GetRequiredService<IOptions<NucleusAspNetCoreMultiTenancyOptions>>().Value;
        }
    }
}





