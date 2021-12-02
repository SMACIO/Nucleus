using Nucleus.DependencyInjection;

namespace Nucleus.MultiTenancy
{
    public class NullTenantResolveResultAccessor : ITenantResolveResultAccessor, ISingletonDependency
    {
        public TenantResolveResult Result
        {
            get => null;
            set { }
        }
    }
}

