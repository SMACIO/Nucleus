using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.Modularity
{
    public interface IModuleContainer
    {
        [NotNull]
        IReadOnlyList<INucleusModuleDescriptor> Modules { get; }
    }
}

