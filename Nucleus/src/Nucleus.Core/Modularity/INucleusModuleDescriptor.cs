using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nucleus.Modularity
{
    public interface INucleusModuleDescriptor
    {
        Type Type { get; }

        Assembly Assembly { get; }

        INucleusModule Instance { get; }

        bool IsLoadedAsPlugIn { get; }

        IReadOnlyList<INucleusModuleDescriptor> Dependencies { get; }
    }
}



