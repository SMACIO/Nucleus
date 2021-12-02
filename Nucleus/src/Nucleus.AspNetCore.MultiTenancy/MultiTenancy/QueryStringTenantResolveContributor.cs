using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nucleus.MultiTenancy;

namespace Nucleus.AspNetCore.MultiTenancy
{
    public class QueryStringTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override Task<string> GetTenantIdOrNameFromHttpContextOrNullAsync(ITenantResolveContext context, HttpContext httpContext)
        {
            if (httpContext.Request.QueryString.HasValue)
            {
                var tenantKey = context.GetNucleusAspNetCoreMultiTenancyOptions().TenantKey;
                if (httpContext.Request.Query.ContainsKey(tenantKey))
                {
                    var tenantValue = httpContext.Request.Query[tenantKey].ToString();
                    if (tenantValue.IsNullOrWhiteSpace())
                    {
                        context.Handled = true;
                        return Task.FromResult<string>(null);
                    }

                    return Task.FromResult(tenantValue);
                }
            }

            return Task.FromResult<string>(null);
        }
    }
}



