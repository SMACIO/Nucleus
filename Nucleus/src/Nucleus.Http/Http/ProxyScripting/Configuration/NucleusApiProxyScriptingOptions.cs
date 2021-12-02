using System;
using System.Collections.Generic;

namespace Nucleus.Http.ProxyScripting.Configuration
{
    public class NucleusApiProxyScriptingOptions
    {
        public IDictionary<string, Type> Generators { get; }

        public NucleusApiProxyScriptingOptions()
        {
            Generators = new Dictionary<string, Type>();
        }
    }
}


