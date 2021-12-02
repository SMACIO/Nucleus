using Castle.DynamicProxy;
using Nucleus.DynamicProxy;

namespace Nucleus.Castle.DynamicProxy
{
    public class NucleusAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
        where TInterceptor : INucleusInterceptor
    {
        public NucleusAsyncDeterminationInterceptor(TInterceptor nucleusInterceptor)
            : base(new CastleAsyncNucleusInterceptorAdapter<TInterceptor>(nucleusInterceptor))
        {

        }
    }
}







