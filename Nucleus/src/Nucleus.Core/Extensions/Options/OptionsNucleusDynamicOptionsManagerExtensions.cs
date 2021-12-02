using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Nucleus;
using Nucleus.Options;

namespace Microsoft.Extensions.Options
{
    public static class OptionsNucleusDynamicOptionsManagerExtensions
    {
        public static Task SetAsync<T>(this IOptions<T> options)
            where T : class
        {
            return options.ToDynamicOptions().SetAsync();
        }

        public static Task SetAsync<T>(this IOptions<T> options, string name)
            where T : class
        {
            return options.ToDynamicOptions().SetAsync(name);
        }

        private static NucleusDynamicOptionsManager<T> ToDynamicOptions<T>(this IOptions<T> options)
            where T : class
        {
            if (options is NucleusDynamicOptionsManager<T> dynamicOptionsManager)
            {
                return dynamicOptionsManager;
            }

            throw new NucleusException($"Options must be derived from the {typeof(NucleusDynamicOptionsManager<>).FullName}!");
        }
    }
}




