using System.Threading.Tasks;

namespace Nucleus.Data
{
    public interface IDataSeeder
    {
        Task SeedAsync(DataSeedContext context);
    }
}
