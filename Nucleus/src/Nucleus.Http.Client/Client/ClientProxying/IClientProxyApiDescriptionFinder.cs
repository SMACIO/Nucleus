using Nucleus.Http.Modeling;

namespace Nucleus.Http.Client.ClientProxying
{
    public interface IClientProxyApiDescriptionFinder
    {
        ActionApiDescriptionModel FindAction(string methodName);

        ApplicationApiDescriptionModel GetApiDescription();
    }
}


