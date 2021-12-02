using System;
using System.Threading.Tasks;
using Nucleus.Http.Modeling;

namespace Nucleus.Http.Client.DynamicProxying
{
    public interface IApiDescriptionCache
    {
        Task<ApplicationApiDescriptionModel> GetAsync(
            string baseUrl,
            Func<Task<ApplicationApiDescriptionModel>> factory
        );
    }
}

