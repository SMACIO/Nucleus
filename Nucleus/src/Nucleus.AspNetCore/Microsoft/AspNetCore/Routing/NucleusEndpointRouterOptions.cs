using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Routing
{
    public class NucleusEndpointRouterOptions
    {
        public List<Action<EndpointRouteBuilderContext>> EndpointConfigureActions { get; }

        public NucleusEndpointRouterOptions()
        {
            EndpointConfigureActions = new List<Action<EndpointRouteBuilderContext>>();
        }
    }
}


