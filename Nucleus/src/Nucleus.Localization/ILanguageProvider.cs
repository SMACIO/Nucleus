using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nucleus.Localization
{
    public interface ILanguageProvider
    {
        Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync();
    }
}

