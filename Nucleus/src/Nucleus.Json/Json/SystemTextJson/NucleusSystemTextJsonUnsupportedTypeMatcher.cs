using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Json.SystemTextJson
{
    public class NucleusSystemTextJsonUnsupportedTypeMatcher : ITransientDependency
    {
        protected NucleusSystemTextJsonSerializerOptions Options { get; }

        public NucleusSystemTextJsonUnsupportedTypeMatcher(IOptions<NucleusSystemTextJsonSerializerOptions> options)
        {
            Options = options.Value;
        }

        public virtual bool Match([CanBeNull]Type type)
        {
            return Options.UnsupportedTypes.Contains(type);
        }
    }
}





