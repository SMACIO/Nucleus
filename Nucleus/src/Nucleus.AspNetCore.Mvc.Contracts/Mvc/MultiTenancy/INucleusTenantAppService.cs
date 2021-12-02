using System;
using System.Threading.Tasks;
using Nucleus.Application.Services;

namespace Nucleus.AspNetCore.Mvc.MultiTenancy
{
    public interface INucleusTenantAppService : IApplicationService
    {
        Task<FindTenantResultDto> FindTenantByNameAsync(string name);

        Task<FindTenantResultDto> FindTenantByIdAsync(Guid id);
    }
}


