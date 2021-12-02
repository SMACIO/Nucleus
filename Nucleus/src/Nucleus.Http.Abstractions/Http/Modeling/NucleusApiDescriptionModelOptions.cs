using System;
using System.Collections.Generic;
using Nucleus.Aspects;
using Nucleus.DependencyInjection;

namespace Nucleus.Http.Modeling
{
    public class NucleusApiDescriptionModelOptions
    {
        public HashSet<Type> IgnoredInterfaces { get; }

        public NucleusApiDescriptionModelOptions()
        {
            IgnoredInterfaces = new HashSet<Type>
            {
                typeof(ITransientDependency),
                typeof(ISingletonDependency),
                typeof(IDisposable),
                typeof(IAvoidDuplicateCrossCuttingConcerns)
            };
        }
    }
}




