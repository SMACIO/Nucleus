using Microsoft.Extensions.Localization;

namespace Nucleus.Localization
{
    public interface ITemplateLocalizer
    {
        string Localize(IStringLocalizer localizer, string text);
    }
}
