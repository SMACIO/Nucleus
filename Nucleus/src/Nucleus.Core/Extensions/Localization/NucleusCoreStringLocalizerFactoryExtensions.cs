using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.Localization
{
    public static class NucleusCoreStringLocalizerFactoryExtensions
    {
        public static IStringLocalizer Create<TResource>(this IStringLocalizerFactory localizerFactory)
        {
            return localizerFactory.Create(typeof(TResource));
        }
    }
}

