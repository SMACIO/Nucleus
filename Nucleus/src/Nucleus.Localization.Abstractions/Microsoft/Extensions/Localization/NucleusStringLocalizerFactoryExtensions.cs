namespace Microsoft.Extensions.Localization
{
    public static class NucleusStringLocalizerFactoryExtensions
    {
        public static IStringLocalizer CreateDefaultOrNull(this IStringLocalizerFactory localizerFactory)
        {
            return (localizerFactory as INucleusStringLocalizerFactoryWithDefaultResourceSupport)
                ?.CreateDefaultOrNull();
        }
    }
}


