using System.Threading.Tasks;

namespace Nucleus.Validation
{
    public interface IObjectValidationContributor
    {
        Task AddErrorsAsync(ObjectValidationContext context);
    }
}

