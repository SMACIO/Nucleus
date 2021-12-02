using System;
using Nucleus.DependencyInjection;

namespace Nucleus.Tracing
{
    public class DefaultCorrelationIdProvider : ICorrelationIdProvider, ISingletonDependency
    {
        public string Get()
        {
            return CreateNewCorrelationId();
        }

        protected virtual string CreateNewCorrelationId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}

