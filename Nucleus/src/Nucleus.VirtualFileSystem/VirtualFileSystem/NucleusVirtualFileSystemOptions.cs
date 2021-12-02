namespace Nucleus.VirtualFileSystem
{
    public class NucleusVirtualFileSystemOptions
    {
        public VirtualFileSetList FileSets { get; }
        
        public NucleusVirtualFileSystemOptions()
        {
            FileSets = new VirtualFileSetList();
        }
    }
}


