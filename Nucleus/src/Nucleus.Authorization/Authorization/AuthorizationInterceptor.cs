using System.Threading.Tasks;
using Nucleus.DependencyInjection;
using Nucleus.DynamicProxy;

namespace Nucleus.Authorization
{
    public class AuthorizationInterceptor : NucleusInterceptor, ITransientDependency
    {
        private readonly IMethodInvocationAuthorizationService _methodInvocationAuthorizationService;

        public AuthorizationInterceptor(IMethodInvocationAuthorizationService methodInvocationAuthorizationService)
        {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public override async Task InterceptAsync(INucleusMethodInvocation invocation)
        {
            await AuthorizeAsync(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual async Task AuthorizeAsync(INucleusMethodInvocation invocation)
        {
            await _methodInvocationAuthorizationService.CheckAsync(
                new MethodInvocationAuthorizationContext(
                    invocation.Method
                )
            );
        }
    }
}




