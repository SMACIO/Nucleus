using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Microsoft.AspNetCore.Cors
{
    public static class NucleusCorsPolicyBuilderExtensions
    {
        public static CorsPolicyBuilder WithNucleusExposedHeaders(this CorsPolicyBuilder corsPolicyBuilder)
        {
            return corsPolicyBuilder.WithExposedHeaders("_NucleusErrorFormat");
        }
    }
}

