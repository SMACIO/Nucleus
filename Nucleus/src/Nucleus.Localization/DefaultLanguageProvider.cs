using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        protected NucleusLocalizationOptions Options { get; }

        public DefaultLanguageProvider(IOptions<NucleusLocalizationOptions> options)
        {
            Options = options.Value;
        }

        public Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync()
        {
            return Task.FromResult((IReadOnlyList<LanguageInfo>)Options.Languages);
        }
    }
}



