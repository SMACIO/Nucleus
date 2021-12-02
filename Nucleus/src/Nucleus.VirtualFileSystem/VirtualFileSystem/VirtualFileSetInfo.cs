using JetBrains.Annotations;
using Microsoft.Extensions.FileProviders;

namespace Nucleus.VirtualFileSystem
{
    public class VirtualFileSetInfo
    {
        public IFileProvider FileProvider { get; }

        public VirtualFileSetInfo([NotNull] IFileProvider fileProvider)
        {
            FileProvider = Check.NotNull(fileProvider, nameof(fileProvider));
        }
    }
}
