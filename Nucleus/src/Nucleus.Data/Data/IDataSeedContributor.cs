using System.Threading.Tasks;

namespace Nucleus.Data
{
    public interface IDataSeedContributor
    {
        Task SeedAsync(DataSeedContext context);
    }
}
