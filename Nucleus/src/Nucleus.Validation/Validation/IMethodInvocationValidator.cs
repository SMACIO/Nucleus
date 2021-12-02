using System.Threading.Tasks;

namespace Nucleus.Validation
{
    public interface IMethodInvocationValidator
    {
        Task ValidateAsync(MethodInvocationValidationContext context);
    }
}

