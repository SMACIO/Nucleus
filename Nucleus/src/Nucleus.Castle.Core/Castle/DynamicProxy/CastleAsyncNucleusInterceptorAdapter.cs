using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Nucleus.DynamicProxy;

namespace Nucleus.Castle.DynamicProxy
{
    public class CastleAsyncNucleusInterceptorAdapter<TInterceptor> : AsyncInterceptorBase
        where TInterceptor : INucleusInterceptor
    {
        private readonly TInterceptor _nucleusInterceptor;

        public CastleAsyncNucleusInterceptorAdapter(TInterceptor nucleusInterceptor)
        {
            _nucleusInterceptor = nucleusInterceptor;
        }

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            await _nucleusInterceptor.InterceptAsync(
                new CastleNucleusMethodInvocationAdapter(invocation, proceedInfo, proceed)
            );
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var adapter = new CastleNucleusMethodInvocationAdapterWithReturnValue<TResult>(invocation, proceedInfo, proceed);

            await _nucleusInterceptor.InterceptAsync(
                adapter
            );

            return (TResult)adapter.ReturnValue;
        }
    }
}





