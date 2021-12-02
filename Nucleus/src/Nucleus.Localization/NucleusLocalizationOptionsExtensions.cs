﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Nucleus.Localization
{
    public static class NucleusLocalizationOptionsExtensions
    {
        public static NucleusLocalizationOptions AddLanguagesMapOrUpdate(this NucleusLocalizationOptions localizationOptions,
            string packageName, params NameValue[] maps)
        {
            foreach (var map in maps)
            {
                AddOrUpdate(localizationOptions.LanguagesMap, packageName, map);
            }

            return localizationOptions;
        }

        public static string GetLanguagesMap(this NucleusLocalizationOptions localizationOptions, string packageName,
            string language)
        {
            return localizationOptions.LanguagesMap.TryGetValue(packageName, out var maps)
                ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
                : language;
        }

        public static string GetCurrentUICultureLanguagesMap(this NucleusLocalizationOptions localizationOptions, string packageName)
        {
            return GetLanguagesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
        }

        public static NucleusLocalizationOptions AddLanguageFilesMapOrUpdate(this NucleusLocalizationOptions localizationOptions,
            string packageName, params NameValue[] maps)
        {
            foreach (var map in maps)
            {
                AddOrUpdate(localizationOptions.LanguageFilesMap, packageName, map);
            }

            return localizationOptions;
        }

        public static string GetLanguageFilesMap(this NucleusLocalizationOptions localizationOptions, string packageName,
            string language)
        {
            return localizationOptions.LanguageFilesMap.TryGetValue(packageName, out var maps)
                ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
                : language;
        }

        public static string GetCurrentUICultureLanguageFilesMap(this NucleusLocalizationOptions localizationOptions, string packageName)
        {
            return GetLanguageFilesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
        }

        private static void AddOrUpdate(IDictionary<string, List<NameValue>> maps, string packageName, NameValue value)
        {
            if (maps.TryGetValue(packageName, out var existMaps))
            {
                existMaps.GetOrAdd(x => x.Name == value.Name, () => value).Value = value.Value;
            }
            else
            {
                maps.Add(packageName, new List<NameValue> {value});
            }
        }
    }
}



