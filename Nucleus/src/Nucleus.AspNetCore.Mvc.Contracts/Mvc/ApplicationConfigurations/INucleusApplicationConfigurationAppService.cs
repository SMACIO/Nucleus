using System.Threading.Tasks;
using Nucleus.Application.Services;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations
{
    public interface INucleusApplicationConfigurationAppService : IApplicationService
    {
        Task<ApplicationConfigurationDto> GetAsync();
    }
}


