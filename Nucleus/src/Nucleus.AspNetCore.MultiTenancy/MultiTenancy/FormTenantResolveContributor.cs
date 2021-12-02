using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nucleus.MultiTenancy;

namespace Nucleus.AspNetCore.MultiTenancy
{
    public class FormTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Form";

        public override string Name => ContributorName;

        protected override async Task<string> GetTenantIdOrNameFromHttpContextOrNullAsync(ITenantResolveContext context, HttpContext httpContext)
        {
            if (!httpContext.Request.HasFormContentType)
            {
                return null;
            }

            var form = await httpContext.Request.ReadFormAsync();
            return form[context.GetNucleusAspNetCoreMultiTenancyOptions().TenantKey];
        }
    }
}



