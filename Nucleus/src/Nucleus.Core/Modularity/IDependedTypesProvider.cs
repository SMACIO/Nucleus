using System;
using JetBrains.Annotations;

namespace Nucleus.Modularity
{
    public interface IDependedTypesProvider
    {
        [NotNull]
        Type[] GetDependedTypes();
    }
}
