using System.Diagnostics;

namespace System.Reflection
{
    public static class NucleusAssemblyExtensions
    {
        public static string GetFileVersion(this Assembly assembly)
        {
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }
    }
}

