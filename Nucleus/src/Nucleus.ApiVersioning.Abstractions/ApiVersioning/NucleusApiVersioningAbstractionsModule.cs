using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;

namespace Nucleus.ApiVersioning
{
    public class NucleusApiVersioningAbstractionsModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IRequestedApiVersion>(NullRequestedApiVersion.Instance);
        }
    }
}




