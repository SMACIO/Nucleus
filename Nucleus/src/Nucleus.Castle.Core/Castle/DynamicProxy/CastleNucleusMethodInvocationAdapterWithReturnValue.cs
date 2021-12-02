using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Nucleus.DynamicProxy;

namespace Nucleus.Castle.DynamicProxy
{
    public class CastleNucleusMethodInvocationAdapterWithReturnValue<TResult> : CastleNucleusMethodInvocationAdapterBase, INucleusMethodInvocation
    {
        protected IInvocationProceedInfo ProceedInfo { get; }
        protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

        public CastleNucleusMethodInvocationAdapterWithReturnValue(IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
            : base(invocation)
        {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async Task ProceedAsync()
        {
            ReturnValue = await Proceed(Invocation, ProceedInfo);
        }
    }
}




