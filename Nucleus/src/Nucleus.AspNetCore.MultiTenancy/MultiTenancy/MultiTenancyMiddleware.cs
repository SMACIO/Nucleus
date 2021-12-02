using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Localization;
using Nucleus.MultiTenancy;
using Nucleus.Settings;

namespace Nucleus.AspNetCore.MultiTenancy
{
    public class MultiTenancyMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ITenantConfigurationProvider _tenantConfigurationProvider;
        private readonly ICurrentTenant _currentTenant;
        private readonly NucleusAspNetCoreMultiTenancyOptions _options;
        private readonly ITenantResolveResultAccessor _tenantResolveResultAccessor;

        public MultiTenancyMiddleware(
            ITenantConfigurationProvider tenantConfigurationProvider,
            ICurrentTenant currentTenant,
            IOptions<NucleusAspNetCoreMultiTenancyOptions> options,
            ITenantResolveResultAccessor tenantResolveResultAccessor)
        {
            _tenantConfigurationProvider = tenantConfigurationProvider;
            _currentTenant = currentTenant;
            _tenantResolveResultAccessor = tenantResolveResultAccessor;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            TenantConfiguration tenant;
            try
            {
                tenant = await _tenantConfigurationProvider.GetAsync(saveResolveResult: true);
            }
            catch (Exception e)
            {
                await _options.MultiTenancyMiddlewareErrorPageBuilder(context, e);
                return;
            }

            if (tenant?.Id != _currentTenant.Id)
            {
                using (_currentTenant.Change(tenant?.Id, tenant?.Name))
                {
                    if (_tenantResolveResultAccessor.Result != null &&
                        _tenantResolveResultAccessor.Result.AppliedResolvers.Contains(QueryStringTenantResolveContributor.ContributorName))
                    {
                        NucleusMultiTenancyCookieHelper.SetTenantCookie(context, _currentTenant.Id, _options.TenantKey);
                    }

                    var requestCulture = await TryGetRequestCultureAsync(context);
                    if (requestCulture != null)
                    {
                        CultureInfo.CurrentCulture = requestCulture.Culture;
                        CultureInfo.CurrentUICulture = requestCulture.UICulture;
                        NucleusRequestCultureCookieHelper.SetCultureCookie(
                            context,
                            requestCulture
                        );
                        context.Items[NucleusRequestLocalizationMiddleware.HttpContextItemName] = true;
                    }

                    await next(context);
                }
            }
            else
            {
                await next(context);
            }
        }

        private async Task<RequestCulture> TryGetRequestCultureAsync(HttpContext httpContext)
        {
            var requestCultureFeature = httpContext.Features.Get<IRequestCultureFeature>();

            /* If requestCultureFeature == null, that means the RequestLocalizationMiddleware was not used
             * and we don't want to set the culture. */
            if (requestCultureFeature == null)
            {
                return null;
            }

            /* If requestCultureFeature.Provider is not null, that means RequestLocalizationMiddleware
             * already picked a language, so we don't need to set the default. */
            if (requestCultureFeature.Provider != null)
            {
                return null;
            }

            var settingProvider = httpContext.RequestServices.GetRequiredService<ISettingProvider>();
            var defaultLanguage = await settingProvider.GetOrNullAsync(LocalizationSettingNames.DefaultLanguage);
            if (defaultLanguage.IsNullOrWhiteSpace())
            {
                return null;
            }

            string culture;
            string uiCulture;

            if (defaultLanguage.Contains(';'))
            {
                var splitted = defaultLanguage.Split(';');
                culture = splitted[0];
                uiCulture = splitted[1];
            }
            else
            {
                culture = defaultLanguage;
                uiCulture = defaultLanguage;
            }

            return new RequestCulture(culture, uiCulture);
        }
    }
}





