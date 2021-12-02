using System.Net.Http;

namespace Nucleus.Http.Client.Proxying
{
    public interface IProxyHttpClientFactory
    {
        HttpClient Create();

        HttpClient Create(string name);
    }
}

