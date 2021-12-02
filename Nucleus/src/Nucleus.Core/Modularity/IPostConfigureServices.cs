using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Modularity
{
    public interface IPostConfigureServices
    {
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
