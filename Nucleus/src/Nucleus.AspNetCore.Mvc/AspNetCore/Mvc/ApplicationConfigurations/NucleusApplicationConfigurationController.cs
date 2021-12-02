using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nucleus.AspNetCore.Mvc.AntiForgery;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations
{
    [Area("nucleus")]
    [RemoteService(Name = "nucleus")]
    [Route("api/nucleus/application-configuration")]
    public class NucleusApplicationConfigurationController : NucleusControllerBase, INucleusApplicationConfigurationAppService
    {
        private readonly INucleusApplicationConfigurationAppService _applicationConfigurationAppService;
        private readonly INucleusAntiForgeryManager _antiForgeryManager;

        public NucleusApplicationConfigurationController(
            INucleusApplicationConfigurationAppService applicationConfigurationAppService,
            INucleusAntiForgeryManager antiForgeryManager)
        {
            _applicationConfigurationAppService = applicationConfigurationAppService;
            _antiForgeryManager = antiForgeryManager;
        }

        [HttpGet]
        public async Task<ApplicationConfigurationDto> GetAsync()
        {
            _antiForgeryManager.SetCookie();
            return await _applicationConfigurationAppService.GetAsync();
        }
    }
}









