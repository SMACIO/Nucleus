using System;
using JetBrains.Annotations;

namespace Nucleus.Localization
{
    public interface IInheritedResourceTypesProvider
    {
        [NotNull]
        Type[] GetInheritedResourceTypes();
    }
}
