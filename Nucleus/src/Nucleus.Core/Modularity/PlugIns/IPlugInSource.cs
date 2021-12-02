using System;
using JetBrains.Annotations;

namespace Nucleus.Modularity.PlugIns
{
    public interface IPlugInSource
    {
        [NotNull]
        Type[] GetModules();
    }
}
