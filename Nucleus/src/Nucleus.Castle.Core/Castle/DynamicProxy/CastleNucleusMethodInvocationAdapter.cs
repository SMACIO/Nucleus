using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Nucleus.DynamicProxy;

namespace Nucleus.Castle.DynamicProxy
{
    public class CastleNucleusMethodInvocationAdapter : CastleNucleusMethodInvocationAdapterBase, INucleusMethodInvocation
    {
        protected IInvocationProceedInfo ProceedInfo { get; }
        protected Func<IInvocation, IInvocationProceedInfo, Task> Proceed { get; }

        public CastleNucleusMethodInvocationAdapter(IInvocation invocation, IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task> proceed)
            : base(invocation)
        {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async Task ProceedAsync()
        {
            await Proceed(Invocation, ProceedInfo);
        }
    }
}




