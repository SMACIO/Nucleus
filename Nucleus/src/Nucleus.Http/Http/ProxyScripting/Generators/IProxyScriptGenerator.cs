using Nucleus.Http.Modeling;

namespace Nucleus.Http.ProxyScripting.Generators
{
    public interface IProxyScriptGenerator
    {
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}

