using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Nucleus.Options;

namespace Microsoft.AspNetCore.RequestLocalization
{
    public class NucleusRequestLocalizationOptionsManager : NucleusDynamicOptionsManager<RequestLocalizationOptions>
    {
        private RequestLocalizationOptions _options;

        private readonly INucleusRequestLocalizationOptionsProvider _nucleusRequestLocalizationOptionsProvider;

        public NucleusRequestLocalizationOptionsManager(
            IOptionsFactory<RequestLocalizationOptions> factory,
            INucleusRequestLocalizationOptionsProvider nucleusRequestLocalizationOptionsProvider)
            : base(factory)
        {
            _nucleusRequestLocalizationOptionsProvider = nucleusRequestLocalizationOptionsProvider;
        }

        public override RequestLocalizationOptions Get(string name)
        {
            return _options ?? base.Get(name);
        }

        protected override async Task OverrideOptionsAsync(string name, RequestLocalizationOptions options)
        {
            _options = await _nucleusRequestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
        }
    }
}







