using JetBrains.Annotations;

namespace Microsoft.Extensions.Localization
{
    public interface INucleusStringLocalizerFactoryWithDefaultResourceSupport
    {
        [CanBeNull]
        IStringLocalizer CreateDefaultOrNull();
    }
}
