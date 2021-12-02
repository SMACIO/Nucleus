using System.Threading.Tasks;

namespace Nucleus.Http.Client.ClientProxying
{
    public interface IObjectToQueryString<in TValue>
    {
        Task<string> ConvertAsync(TValue value);
    }
}

