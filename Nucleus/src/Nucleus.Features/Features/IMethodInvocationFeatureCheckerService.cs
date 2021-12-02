using System.Threading.Tasks;

namespace Nucleus.Features
{
    public interface IMethodInvocationFeatureCheckerService
    {
        Task CheckAsync(
            MethodInvocationFeatureCheckerContext context
        );
    }
}
