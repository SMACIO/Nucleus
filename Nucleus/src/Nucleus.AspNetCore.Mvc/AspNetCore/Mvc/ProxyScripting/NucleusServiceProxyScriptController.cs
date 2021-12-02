using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nucleus.Auditing;
using Nucleus.Http;
using Nucleus.Http.ProxyScripting;
using Nucleus.Minify.Scripts;

namespace Nucleus.AspNetCore.Mvc.ProxyScripting
{
    [Area("Nucleus")]
    [Route("Nucleus/ServiceProxyScript")]
    [DisableAuditing]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NucleusServiceProxyScriptController : NucleusController
    {
        private readonly IProxyScriptManager _proxyScriptManager;
        private readonly NucleusAspNetCoreMvcOptions _options;
        private readonly IJavascriptMinifier _javascriptMinifier;

        public NucleusServiceProxyScriptController(IProxyScriptManager proxyScriptManager,
            IOptions<NucleusAspNetCoreMvcOptions> options,
            IJavascriptMinifier javascriptMinifier)
        {
            _proxyScriptManager = proxyScriptManager;
            _options = options.Value;
            _javascriptMinifier = javascriptMinifier;
        }

        [HttpGet]
        [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
        public ActionResult GetAll(ServiceProxyGenerationModel model)
        {
            model.Normalize();

            var script = _proxyScriptManager.GetScript(model.CreateOptions());

            return Content(
                _options.MinifyGeneratedScript == true
                    ? _javascriptMinifier.Minify(script)
                    : script,
                MimeTypes.Application.Javascript
            );
        }
    }
}







