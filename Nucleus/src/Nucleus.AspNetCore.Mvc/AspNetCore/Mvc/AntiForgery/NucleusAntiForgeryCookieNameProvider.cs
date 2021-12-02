using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.AspNetCore.Mvc.AntiForgery
{
    public class NucleusAntiForgeryCookieNameProvider : ITransientDependency
    {
        private readonly IOptionsMonitor<CookieAuthenticationOptions> _namedOptionsAccessor;
        private readonly NucleusAntiForgeryOptions _nucleusAntiForgeryOptions;

        public NucleusAntiForgeryCookieNameProvider(
            IOptionsMonitor<CookieAuthenticationOptions> namedOptionsAccessor,
            IOptions<NucleusAntiForgeryOptions> nucleusAntiForgeryOptions)
        {
            _namedOptionsAccessor = namedOptionsAccessor;
            _nucleusAntiForgeryOptions = nucleusAntiForgeryOptions.Value;
        }

        public virtual string GetAuthCookieNameOrNull()
        {
            if (_nucleusAntiForgeryOptions.AuthCookieSchemaName == null)
            {
                return null;
            }

            return _namedOptionsAccessor.Get(_nucleusAntiForgeryOptions.AuthCookieSchemaName)?.Cookie?.Name;
        }

        public virtual string GetAntiForgeryCookieNameOrNull()
        {
            return _nucleusAntiForgeryOptions.TokenCookie.Name;
        }
    }
}







