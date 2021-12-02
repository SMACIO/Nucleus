using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nucleus.AspNetCore.Mvc.AntiForgery;
using Nucleus.Auditing;
using Nucleus.Http;
using Nucleus.Json;
using Nucleus.Minify.Scripts;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations
{
    [Area("Nucleus")]
    [Route("Nucleus/ApplicationConfigurationScript")]
    [DisableAuditing]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NucleusApplicationConfigurationScriptController : NucleusController
    {
        private readonly INucleusApplicationConfigurationAppService _configurationAppService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly NucleusAspNetCoreMvcOptions _options;
        private readonly IJavascriptMinifier _javascriptMinifier;
        private readonly INucleusAntiForgeryManager _antiForgeryManager;

        public NucleusApplicationConfigurationScriptController(
            INucleusApplicationConfigurationAppService configurationAppService,
            IJsonSerializer jsonSerializer,
            IOptions<NucleusAspNetCoreMvcOptions> options,
            IJavascriptMinifier javascriptMinifier,
            INucleusAntiForgeryManager antiForgeryManager)
        {
            _configurationAppService = configurationAppService;
            _jsonSerializer = jsonSerializer;
            _options = options.Value;
            _javascriptMinifier = javascriptMinifier;
            _antiForgeryManager = antiForgeryManager;
        }

        [HttpGet]
        [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
        public async Task<ActionResult> Get()
        {
            var script = CreateNucleusExtendScript(await _configurationAppService.GetAsync());

            _antiForgeryManager.SetCookie();

            return Content(
                _options.MinifyGeneratedScript == true
                    ? _javascriptMinifier.Minify(script)
                    : script,
                MimeTypes.Application.Javascript
            );
        }

        private string CreateNucleusExtendScript(ApplicationConfigurationDto config)
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine($"$.extend(true, nucleus, {_jsonSerializer.Serialize(config, indented: true)})");
            script.AppendLine();
            script.AppendLine("nucleus.event.trigger('nucleus.configurationInitialized');");
            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}











