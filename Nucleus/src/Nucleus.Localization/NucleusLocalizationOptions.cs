using System;
using System.Collections.Generic;
using Nucleus.Collections;

namespace Nucleus.Localization
{
    public class NucleusLocalizationOptions
    {
        public LocalizationResourceDictionary Resources { get; }

        /// <summary>
        /// Used as the default resource when resource was not specified on a localization operation.
        /// </summary>
        public Type DefaultResourceType { get; set; }

        public ITypeList<ILocalizationResourceContributor> GlobalContributors { get; }

        public List<LanguageInfo> Languages { get; }

        public Dictionary<string, List<NameValue>> LanguagesMap  { get; }

        public Dictionary<string, List<NameValue>> LanguageFilesMap { get; }

        public NucleusLocalizationOptions()
        {
            Resources = new LocalizationResourceDictionary();
            GlobalContributors = new TypeList<ILocalizationResourceContributor>();
            Languages = new List<LanguageInfo>();
            LanguagesMap = new Dictionary<string, List<NameValue>>();
            LanguageFilesMap = new Dictionary<string, List<NameValue>>();
        }
    }
}




