using Microsoft.AspNetCore.Mvc;
using Nucleus.Http.Modeling;

namespace Nucleus.AspNetCore.Mvc.ApiExploring
{
    [Area("nucleus")]
    [RemoteService(Name = "nucleus")]
    [Route("api/nucleus/api-definition")]
    public class NucleusApiDefinitionController : NucleusController, IRemoteService
    {
        private readonly IApiDescriptionModelProvider _modelProvider;

        public NucleusApiDefinitionController(IApiDescriptionModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
        }

        [HttpGet]
        public ApplicationApiDescriptionModel Get(ApplicationApiDescriptionModelRequestDto model)
        {
            return _modelProvider.CreateApiModel(model);
        }
    }
}








