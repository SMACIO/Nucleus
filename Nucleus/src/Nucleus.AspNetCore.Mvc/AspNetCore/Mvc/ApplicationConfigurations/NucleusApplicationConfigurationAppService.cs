﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nucleus.Application.Services;
using Nucleus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Nucleus.AspNetCore.Mvc.MultiTenancy;
using Nucleus.Authorization;
using Nucleus.Authorization.Permissions;
using Nucleus.Features;
using Nucleus.Localization;
using Nucleus.MultiTenancy;
using Nucleus.Settings;
using Nucleus.Timing;
using Nucleus.Users;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations
{
    public class NucleusApplicationConfigurationAppService : ApplicationService, INucleusApplicationConfigurationAppService
    {
        private readonly NucleusLocalizationOptions _localizationOptions;
        private readonly NucleusMultiTenancyOptions _multiTenancyOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly INucleusAuthorizationPolicyProvider _nucleusAuthorizationPolicyProvider;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        private readonly DefaultAuthorizationPolicyProvider _defaultAuthorizationPolicyProvider;
        private readonly IPermissionChecker _permissionChecker;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentUser _currentUser;
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly IFeatureDefinitionManager _featureDefinitionManager;
        private readonly ILanguageProvider _languageProvider;
        private readonly ITimezoneProvider _timezoneProvider;
        private readonly NucleusClockOptions _nucleusClockOptions;
        private readonly ICachedObjectExtensionsDtoService _cachedObjectExtensionsDtoService;

        public NucleusApplicationConfigurationAppService(
            IOptions<NucleusLocalizationOptions> localizationOptions,
            IOptions<NucleusMultiTenancyOptions> multiTenancyOptions,
            IServiceProvider serviceProvider,
            INucleusAuthorizationPolicyProvider nucleusAuthorizationPolicyProvider,
            IPermissionDefinitionManager permissionDefinitionManager,
            DefaultAuthorizationPolicyProvider defaultAuthorizationPolicyProvider,
            IPermissionChecker permissionChecker,
            IAuthorizationService authorizationService,
            ICurrentUser currentUser,
            ISettingProvider settingProvider,
            ISettingDefinitionManager settingDefinitionManager,
            IFeatureDefinitionManager featureDefinitionManager,
            ILanguageProvider languageProvider,
            ITimezoneProvider timezoneProvider,
            IOptions<NucleusClockOptions> nucleusClockOptions,
            ICachedObjectExtensionsDtoService cachedObjectExtensionsDtoService)
        {
            _serviceProvider = serviceProvider;
            _nucleusAuthorizationPolicyProvider = nucleusAuthorizationPolicyProvider;
            _permissionDefinitionManager = permissionDefinitionManager;
            _defaultAuthorizationPolicyProvider = defaultAuthorizationPolicyProvider;
            _permissionChecker = permissionChecker;
            _authorizationService = authorizationService;
            _currentUser = currentUser;
            _settingProvider = settingProvider;
            _settingDefinitionManager = settingDefinitionManager;
            _featureDefinitionManager = featureDefinitionManager;
            _languageProvider = languageProvider;
            _timezoneProvider = timezoneProvider;
            _nucleusClockOptions = nucleusClockOptions.Value;
            _cachedObjectExtensionsDtoService = cachedObjectExtensionsDtoService;
            _localizationOptions = localizationOptions.Value;
            _multiTenancyOptions = multiTenancyOptions.Value;
        }

        public virtual async Task<ApplicationConfigurationDto> GetAsync()
        {
            //TODO: Optimize & cache..?

            Logger.LogDebug("Executing NucleusApplicationConfigurationAppService.GetAsync()...");

            var result = new ApplicationConfigurationDto
            {
                Auth = await GetAuthConfigAsync(),
                Features = await GetFeaturesConfigAsync(),
                Localization = await GetLocalizationConfigAsync(),
                CurrentUser = GetCurrentUser(),
                Setting = await GetSettingConfigAsync(),
                MultiTenancy = GetMultiTenancy(),
                CurrentTenant = GetCurrentTenant(),
                Timing = await GetTimingConfigAsync(),
                Clock = GetClockConfig(),
                ObjectExtensions = _cachedObjectExtensionsDtoService.Get()
            };

            Logger.LogDebug("Executed NucleusApplicationConfigurationAppService.GetAsync().");

            return result;
        }

        protected virtual CurrentTenantDto GetCurrentTenant()
        {
            return new CurrentTenantDto()
            {
                Id = CurrentTenant.Id,
                Name = CurrentTenant.Name,
                IsAvailable = CurrentTenant.IsAvailable
            };
        }

        protected virtual MultiTenancyInfoDto GetMultiTenancy()
        {
            return new MultiTenancyInfoDto
            {
                IsEnabled = _multiTenancyOptions.IsEnabled
            };
        }

        protected virtual CurrentUserDto GetCurrentUser()
        {
            return new CurrentUserDto
            {
                IsAuthenticated = _currentUser.IsAuthenticated,
                Id = _currentUser.Id,
                TenantId = _currentUser.TenantId,
                ImpersonatorUserId = _currentUser.FindImpersonatorUserId(),
                ImpersonatorTenantId = _currentUser.FindImpersonatorTenantId(),
                UserName = _currentUser.UserName,
                SurName = _currentUser.SurName,
                Name = _currentUser.Name,
                Email = _currentUser.Email,
                EmailVerified = _currentUser.EmailVerified,
                PhoneNumber = _currentUser.PhoneNumber,
                PhoneNumberVerified = _currentUser.PhoneNumberVerified,
                Roles = _currentUser.Roles
            };
        }

        protected virtual async Task<ApplicationAuthConfigurationDto> GetAuthConfigAsync()
        {
            var authConfig = new ApplicationAuthConfigurationDto();

            var policyNames = await _nucleusAuthorizationPolicyProvider.GetPoliciesNamesAsync();
            var nucleusPolicyNames = new List<string>();
            var otherPolicyNames = new List<string>();

            foreach (var policyName in policyNames)
            {
                if(await _defaultAuthorizationPolicyProvider.GetPolicyAsync(policyName) == null && _permissionDefinitionManager.GetOrNull(policyName) != null)
                {
                    nucleusPolicyNames.Add(policyName);
                }
                else
                {
                    otherPolicyNames.Add(policyName);
                }
            }

            foreach (var policyName in otherPolicyNames)
            {
                authConfig.Policies[policyName] = true;

                if (await _authorizationService.IsGrantedAsync(policyName))
                {
                    authConfig.GrantedPolicies[policyName] = true;
                }
            }

            var result = await _permissionChecker.IsGrantedAsync(nucleusPolicyNames.ToArray());
            foreach (var (key, value) in result.Result)
            {
                authConfig.Policies[key] = true;
                if (value == PermissionGrantResult.Granted)
                {
                    authConfig.GrantedPolicies[key] = true;
                }
            }

            return authConfig;
        }

        protected virtual async Task<ApplicationLocalizationConfigurationDto> GetLocalizationConfigAsync()
        {
            var localizationConfig = new ApplicationLocalizationConfigurationDto();

            localizationConfig.Languages.AddRange(await _languageProvider.GetLanguagesAsync());

            foreach (var resource in _localizationOptions.Resources.Values)
            {
                var dictionary = new Dictionary<string, string>();

                var localizer = _serviceProvider.GetRequiredService(
                    typeof(IStringLocalizer<>).MakeGenericType(resource.ResourceType)
                ) as IStringLocalizer;

                foreach (var localizedString in localizer.GetAllStrings())
                {
                    dictionary[localizedString.Name] = localizedString.Value;
                }

                localizationConfig.Values[resource.ResourceName] = dictionary;
            }

            localizationConfig.CurrentCulture = GetCurrentCultureInfo();

            if (_localizationOptions.DefaultResourceType != null)
            {
                localizationConfig.DefaultResourceName = LocalizationResourceNameAttribute.GetName(
                    _localizationOptions.DefaultResourceType
                );
            }

            localizationConfig.LanguagesMap = _localizationOptions.LanguagesMap;
            localizationConfig.LanguageFilesMap = _localizationOptions.LanguageFilesMap;

            return localizationConfig;
        }

        private static CurrentCultureDto GetCurrentCultureInfo()
        {
            return new CurrentCultureDto
            {
                Name = CultureInfo.CurrentUICulture.Name,
                DisplayName = CultureInfo.CurrentUICulture.DisplayName,
                EnglishName = CultureInfo.CurrentUICulture.EnglishName,
                NativeName = CultureInfo.CurrentUICulture.NativeName,
                IsRightToLeft = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft,
                CultureName = CultureInfo.CurrentUICulture.TextInfo.CultureName,
                TwoLetterIsoLanguageName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName,
                ThreeLetterIsoLanguageName = CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName,
                DateTimeFormat = new DateTimeFormatDto
                {
                    CalendarAlgorithmType =
                        CultureInfo.CurrentUICulture.DateTimeFormat.Calendar.AlgorithmType.ToString(),
                    DateTimeFormatLong = CultureInfo.CurrentUICulture.DateTimeFormat.LongDatePattern,
                    ShortDatePattern = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern,
                    FullDateTimePattern = CultureInfo.CurrentUICulture.DateTimeFormat.FullDateTimePattern,
                    DateSeparator = CultureInfo.CurrentUICulture.DateTimeFormat.DateSeparator,
                    ShortTimePattern = CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern,
                    LongTimePattern = CultureInfo.CurrentUICulture.DateTimeFormat.LongTimePattern,
                }
            };
        }

        private async Task<ApplicationSettingConfigurationDto> GetSettingConfigAsync()
        {
            var result = new ApplicationSettingConfigurationDto
            {
                Values = new Dictionary<string, string>()
            };

            var settingDefinitions = _settingDefinitionManager.GetAll().Where(x => x.IsVisibleToClients);

            var settingValues = await _settingProvider.GetAllAsync(settingDefinitions.Select(x => x.Name).ToArray());

            foreach (var settingValue in settingValues)
            {
                result.Values[settingValue.Name] = settingValue.Value;
            }

            return result;
        }

        protected virtual async Task<ApplicationFeatureConfigurationDto> GetFeaturesConfigAsync()
        {
            var result = new ApplicationFeatureConfigurationDto();

            foreach (var featureDefinition in _featureDefinitionManager.GetAll())
            {
                if (!featureDefinition.IsVisibleToClients)
                {
                    continue;
                }

                result.Values[featureDefinition.Name] = await FeatureChecker.GetOrNullAsync(featureDefinition.Name);
            }

            return result;
        }

        protected virtual async Task<TimingDto> GetTimingConfigAsync()
        {
            var windowsTimeZoneId = await _settingProvider.GetOrNullAsync(TimingSettingNames.TimeZone);

            return new TimingDto
            {
                TimeZone = new TimeZone
                {
                    Windows = new WindowsTimeZone
                    {
                        TimeZoneId = windowsTimeZoneId
                    },
                    Iana = new IanaTimeZone
                    {
                        TimeZoneName = windowsTimeZoneId.IsNullOrWhiteSpace()
                            ? null
                            : _timezoneProvider.WindowsToIana(windowsTimeZoneId)
                    }
                }
            };
        }

        protected virtual ClockDto GetClockConfig()
        {
            return new ClockDto
            {
                Kind = Enum.GetName(typeof(DateTimeKind), _nucleusClockOptions.Kind)
            };
        }
    }
}









