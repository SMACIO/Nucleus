using System.Threading.Tasks;

namespace Nucleus.Application.Services
{
    public interface ICreateAppService<TEntityDto>
        : ICreateAppService<TEntityDto, TEntityDto>
    {

    }

    public interface ICreateAppService<TGetOutputDto, in TCreateInput>
        : IApplicationService
    {
        Task<TGetOutputDto> CreateAsync(TCreateInput input);
    }
}

