using JetBrains.Annotations;

namespace Nucleus.MultiTenancy
{
    public interface ITenantResolveResultAccessor
    {
        [CanBeNull]
        TenantResolveResult Result { get; set; }
    }
}

