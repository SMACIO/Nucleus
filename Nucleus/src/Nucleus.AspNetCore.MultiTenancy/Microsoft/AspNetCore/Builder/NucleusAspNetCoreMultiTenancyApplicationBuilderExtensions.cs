using Nucleus.AspNetCore.MultiTenancy;

namespace Microsoft.AspNetCore.Builder
{
    public static class NucleusAspNetCoreMultiTenancyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<MultiTenancyMiddleware>();
        }
    }
}


