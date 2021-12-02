using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Localization;
using Nucleus.Settings;
using Nucleus.Threading;

namespace Microsoft.AspNetCore.RequestLocalization
{
    public class DefaultNucleusRequestLocalizationOptionsProvider : INucleusRequestLocalizationOptionsProvider, ISingletonDependency
    {
        private readonly IServiceScopeFactory _serviceProviderFactory;
        private readonly SemaphoreSlim _syncSemaphore;
        private Action<RequestLocalizationOptions> _optionsAction;
        private RequestLocalizationOptions _requestLocalizationOptions;

        public DefaultNucleusRequestLocalizationOptionsProvider(IServiceScopeFactory serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory;
            _syncSemaphore = new SemaphoreSlim(1, 1);
        }

        public void InitLocalizationOptions(Action<RequestLocalizationOptions> optionsAction = null)
        {
            _optionsAction = optionsAction;
        }

        public async Task<RequestLocalizationOptions> GetLocalizationOptionsAsync()
        {
            if (_requestLocalizationOptions == null)
            {
                using (await _syncSemaphore.LockAsync())
                {
                    if (_requestLocalizationOptions == null)
                    {
                        using (var serviceScope = _serviceProviderFactory.CreateScope())
                        {
                            var languageProvider = serviceScope.ServiceProvider.GetRequiredService<ILanguageProvider>();
                            var settingProvider = serviceScope.ServiceProvider.GetRequiredService<ISettingProvider>();

                            var languages = await languageProvider.GetLanguagesAsync();
                            var defaultLanguage = await settingProvider.GetOrNullAsync(LocalizationSettingNames.DefaultLanguage);

                            var options = !languages.Any()
                                ? new RequestLocalizationOptions()
                                : new RequestLocalizationOptions
                                {
                                    DefaultRequestCulture = DefaultGetRequestCulture(defaultLanguage, languages),

                                    SupportedCultures = languages
                                        .Select(l => l.CultureName)
                                        .Distinct()
                                        .Select(c => new CultureInfo(c))
                                        .ToArray(),

                                    SupportedUICultures = languages
                                        .Select(l => l.UiCultureName)
                                        .Distinct()
                                        .Select(c => new CultureInfo(c))
                                        .ToArray()
                                };

                            foreach (var configurator in serviceScope.ServiceProvider
                                .GetRequiredService<IOptions<NucleusRequestLocalizationOptions>>()
                                .Value.RequestLocalizationOptionConfigurators)
                            {
                                await configurator(serviceScope.ServiceProvider, options);
                            }

                            _optionsAction?.Invoke(options);
                            _requestLocalizationOptions = options;
                        }
                    }
                }
            }

            return _requestLocalizationOptions;
        }

        private static RequestCulture DefaultGetRequestCulture(string defaultLanguage, IReadOnlyList<LanguageInfo> languages)
        {
            if (defaultLanguage == null)
            {
                var firstLanguage = languages.First();
                return new RequestCulture(firstLanguage.CultureName, firstLanguage.UiCultureName);
            }

            var (cultureName, uiCultureName) = LocalizationSettingHelper.ParseLanguageSetting(defaultLanguage);
            return new RequestCulture(cultureName, uiCultureName);
        }
    }
}




