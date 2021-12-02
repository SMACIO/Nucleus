using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Uow;

namespace Nucleus.Data
{
    //TODO: Create a Nucleus.Data.Seeding namespace?
    public class DataSeeder : IDataSeeder, ITransientDependency
    {
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected NucleusDataSeedOptions Options { get; }

        public DataSeeder(
            IOptions<NucleusDataSeedOptions> options,
            IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                foreach (var contributorType in Options.Contributors)
                {
                    var contributor = (IDataSeedContributor) scope
                        .ServiceProvider
                        .GetRequiredService(contributorType);

                    await contributor.SeedAsync(context);
                }
            }
        }
    }
}







