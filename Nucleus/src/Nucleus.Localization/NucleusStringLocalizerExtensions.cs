using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.Localization;
using Nucleus.DynamicProxy;
using Nucleus.Reflection;

namespace Nucleus.Localization
{
    public static class NucleusStringLocalizerExtensions
    {
        [NotNull]
        public static IStringLocalizer GetInternalLocalizer(
            [NotNull] this IStringLocalizer stringLocalizer)
        {
            Check.NotNull(stringLocalizer, nameof(stringLocalizer));

            var localizerType = stringLocalizer.GetType();
            if (!ReflectionHelper.IsAssignableToGenericType(localizerType, typeof(StringLocalizer<>)))
            {
                return stringLocalizer;
            }

            var localizerField = localizerType
                .GetField(
                    "_localizer",
                    BindingFlags.Instance | 
                    BindingFlags.NonPublic
                );

            if (localizerField == null)
            {
                throw new NucleusException($"Could not find the _localizer field inside the {typeof(StringLocalizer<>).FullName} class. Probably its name has changed. Please report this issue to the NUCLEUS framework.");
            }

            return localizerField.GetValue(stringLocalizer) as IStringLocalizer;
        }

        public static IEnumerable<LocalizedString> GetAllStrings(
            this IStringLocalizer stringLocalizer,
            bool includeParentCultures,
            bool includeBaseLocalizers)
        {
            var internalLocalizer = (ProxyHelper.UnProxy(stringLocalizer) as IStringLocalizer).GetInternalLocalizer();
            if (internalLocalizer is IStringLocalizerSupportsInheritance stringLocalizerSupportsInheritance)
            {
                return stringLocalizerSupportsInheritance.GetAllStrings(
                    includeParentCultures,
                    includeBaseLocalizers
                );
            }

            return stringLocalizer.GetAllStrings(
                includeParentCultures
            );
        }
    }
}




