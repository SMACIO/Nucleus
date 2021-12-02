using System.Threading.Tasks;

namespace Nucleus.DynamicProxy
{
	public interface INucleusInterceptor
    {
        Task InterceptAsync(INucleusMethodInvocation invocation);
	}
}



