using Microsoft.Extensions.FileProviders;

namespace Nucleus.VirtualFileSystem
{
    public interface IDynamicFileProvider : IFileProvider
    {
        void AddOrUpdate(IFileInfo fileInfo);

        bool Delete(string filePath);
    }
}

