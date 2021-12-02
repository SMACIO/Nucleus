using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.RequestLocalization;
using Nucleus.Localization;

namespace Nucleus.AspNetCore.Mvc.Localization
{
    [Area("Nucleus")]
    [Route("Nucleus/Languages/[action]")]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NucleusLanguagesController : NucleusController
    {
        protected IQueryStringCultureReplacement QueryStringCultureReplacement { get; }

        public NucleusLanguagesController(IQueryStringCultureReplacement queryStringCultureReplacement)
        {
            QueryStringCultureReplacement = queryStringCultureReplacement;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Switch(string culture, string uiCulture = "", string returnUrl = "")
        {
            if (!CultureHelper.IsValidCultureCode(culture))
            {
                throw new NucleusException("Unknown language: " + culture + ". It must be a valid culture!");
            }

            NucleusRequestCultureCookieHelper.SetCultureCookie(
                HttpContext,
                new RequestCulture(culture, uiCulture)
            );

            HttpContext.Items[NucleusRequestLocalizationMiddleware.HttpContextItemName] = true;

            var context = new QueryStringCultureReplacementContext(HttpContext, new RequestCulture(culture, uiCulture), returnUrl);
            await QueryStringCultureReplacement.ReplaceAsync(context);

            if (!string.IsNullOrWhiteSpace(context.ReturnUrl))
            {
                return Redirect(GetRedirectUrl(context.ReturnUrl));
            }

            return Redirect("~/");
        }

        protected virtual string GetRedirectUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                return "~/";
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return "~/";
        }
    }
}







