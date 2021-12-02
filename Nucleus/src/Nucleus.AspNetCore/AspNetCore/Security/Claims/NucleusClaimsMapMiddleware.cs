using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Security.Claims;

namespace Nucleus.AspNetCore.Security.Claims
{
    public class NucleusClaimsMapMiddleware : IMiddleware, ITransientDependency
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var currentPrincipalAccessor = context.RequestServices
                .GetRequiredService<ICurrentPrincipalAccessor>();

            var mapOptions = context.RequestServices
                .GetRequiredService<IOptions<NucleusClaimsMapOptions>>().Value;

            var mapClaims = currentPrincipalAccessor
                .Principal
                .Claims
                .Where(claim => mapOptions.Maps.Keys.Contains(claim.Type));

            currentPrincipalAccessor
                .Principal
                .AddIdentity(
                    new ClaimsIdentity(
                        mapClaims
                            .Select(
                                claim => new Claim(
                                    mapOptions.Maps[claim.Type](),
                                    claim.Value,
                                    claim.ValueType,
                                    claim.Issuer
                                )
                            )
                    )
                );

            await next(context);
        }
    }
}




