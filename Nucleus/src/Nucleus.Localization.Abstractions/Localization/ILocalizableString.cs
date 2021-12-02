using Microsoft.Extensions.Localization;

namespace Nucleus.Localization
{
    public interface ILocalizableString
    {
        LocalizedString Localize(IStringLocalizerFactory stringLocalizerFactory);
    }
}
