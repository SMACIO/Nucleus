using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

namespace Nucleus.AspNetCore.Mvc.Conventions
{
    public class NucleusConventionalApiControllerSpecification : IApiControllerSpecification
    {
        private readonly NucleusAspNetCoreMvcOptions _options;

        public NucleusConventionalApiControllerSpecification(IOptions<NucleusAspNetCoreMvcOptions> options)
        {
            _options = options.Value;
        }

        public bool IsSatisfiedBy(ControllerModel controller)
        {
            var configuration = _options
                .ConventionalControllers
                .ConventionalControllerSettings
                .GetSettingOrNull(controller.ControllerType.AsType());

             return configuration != null;
        }
    }
}



