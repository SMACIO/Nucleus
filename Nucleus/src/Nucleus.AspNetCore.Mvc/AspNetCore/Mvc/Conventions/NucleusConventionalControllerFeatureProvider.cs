using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nucleus.AspNetCore.Mvc.Conventions
{
    public class NucleusConventionalControllerFeatureProvider : ControllerFeatureProvider
    {
        private readonly INucleusApplication _application;

        public NucleusConventionalControllerFeatureProvider(INucleusApplication application)
        {
            _application = application;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            //TODO: Move this to a lazy loaded field for efficiency.
            if (_application.ServiceProvider == null)
            {
                return false;
            }

            var configuration = _application.ServiceProvider
                .GetRequiredService<IOptions<NucleusAspNetCoreMvcOptions>>().Value
                .ConventionalControllers
                .ConventionalControllerSettings
                .GetSettingOrNull(typeInfo.AsType());

            return configuration != null;
        }
    }
}





