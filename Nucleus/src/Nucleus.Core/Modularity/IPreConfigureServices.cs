using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Modularity
{
    public interface IPreConfigureServices
    {
        void PreConfigureServices(ServiceConfigurationContext context);
    }
}

