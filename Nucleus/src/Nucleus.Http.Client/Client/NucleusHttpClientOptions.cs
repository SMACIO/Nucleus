using System;
using System.Collections.Generic;
using Nucleus.Http.Client.DynamicProxying;
using Nucleus.Http.Client.Proxying;

namespace Nucleus.Http.Client
{
    public class NucleusHttpClientOptions
    {
        public Dictionary<Type, HttpClientProxyConfig> HttpClientProxies { get; set; }

        public NucleusHttpClientOptions()
        {
            HttpClientProxies = new Dictionary<Type, HttpClientProxyConfig>();
        }
    }
}




