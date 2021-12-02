using System.Threading.Tasks;
using Nucleus.AspNetCore.Mvc.ApplicationConfigurations;

namespace Nucleus.AspNetCore.Mvc.Client
{
    public interface ICachedApplicationConfigurationClient : IAsyncInitialize
    {
        Task<ApplicationConfigurationDto> GetAsync();

        ApplicationConfigurationDto Get();
    }
}


