using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Nucleus.Localization.VirtualFiles.Json;

namespace Nucleus.Localization
{
    public static class LocalizationResourceExtensions
    {
        public static LocalizationResource AddVirtualJson(
            [NotNull] this LocalizationResource localizationResource,
            [NotNull] string virtualPath)
        {
            Check.NotNull(localizationResource, nameof(localizationResource));
            Check.NotNull(virtualPath, nameof(virtualPath));

            localizationResource.Contributors.Add(new JsonVirtualFileLocalizationResourceContributor(
                virtualPath.EnsureStartsWith('/')
            ));

            return localizationResource;
        }

        public static LocalizationResource AddBaseTypes(
            [NotNull] this LocalizationResource localizationResource,
            [NotNull] params Type[] types)
        {
            Check.NotNull(localizationResource, nameof(localizationResource));
            Check.NotNull(types, nameof(types));

            foreach (var type in types)
            {
                localizationResource.BaseResourceTypes.AddIfNotContains(type);
            }

            return localizationResource;
        }
    }
}

