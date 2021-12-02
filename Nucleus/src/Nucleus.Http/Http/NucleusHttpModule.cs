using Nucleus.Http.ProxyScripting.Configuration;
using Nucleus.Http.ProxyScripting.Generators.JQuery;
using Nucleus.Json;
using Nucleus.Minify;
using Nucleus.Modularity;

namespace Nucleus.Http
{
    [DependsOn(typeof(NucleusHttpAbstractionsModule))]
    [DependsOn(typeof(NucleusJsonModule))]
    [DependsOn(typeof(NucleusMinifyModule))]
    public class NucleusHttpModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusApiProxyScriptingOptions>(options =>
            {
                options.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);
            });
        }
    }
}

