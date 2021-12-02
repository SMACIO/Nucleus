using System.Collections.Generic;

namespace Nucleus.Settings
{
    public interface ISettingValueProviderManager
    {
        List<ISettingValueProvider> Providers { get; }
    }
}
