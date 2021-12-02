using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Nucleus.Localization
{
    public interface IStringLocalizerSupportsInheritance
    {
        IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, bool includeBaseLocalizers);
    }
}
