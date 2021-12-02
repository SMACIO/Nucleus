using System.Collections.Generic;

namespace Nucleus.Authorization.Permissions
{
    public interface IPermissionValueProviderManager
    {
        IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
    }
}
