using System.Threading.Tasks;

namespace Nucleus.Application.Services
{
    public interface IDeleteAppService<in TKey> : IApplicationService
    {
        Task DeleteAsync(TKey id);
    }
}

