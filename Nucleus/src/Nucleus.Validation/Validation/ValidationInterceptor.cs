using System.Threading.Tasks;
using Nucleus.DependencyInjection;
using Nucleus.DynamicProxy;

namespace Nucleus.Validation
{
    public class ValidationInterceptor : NucleusInterceptor, ITransientDependency
    {
        private readonly IMethodInvocationValidator _methodInvocationValidator;

        public ValidationInterceptor(IMethodInvocationValidator methodInvocationValidator)
        {
            _methodInvocationValidator = methodInvocationValidator;
        }

        public override async Task InterceptAsync(INucleusMethodInvocation invocation)
        {
            await ValidateAsync(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual async Task ValidateAsync(INucleusMethodInvocation invocation)
        {
            await _methodInvocationValidator.ValidateAsync(
                new MethodInvocationValidationContext(
                    invocation.TargetObject,
                    invocation.Method,
                    invocation.Arguments
                )
            );
        }
    }
}




