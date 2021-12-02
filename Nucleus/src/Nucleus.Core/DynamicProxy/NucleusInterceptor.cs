using System.Threading.Tasks;

namespace Nucleus.DynamicProxy
{
	public abstract class NucleusInterceptor : INucleusInterceptor
    {
        public abstract Task InterceptAsync(INucleusMethodInvocation invocation);
    }
}



