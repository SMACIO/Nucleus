using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Http.Client
{
    public class NucleusHttpClientBuilderOptions
    {
        public List<Action<string, IHttpClientBuilder>> ProxyClientBuildActions { get; }

        internal HashSet<string> ConfiguredProxyClients { get; }

        public List<Action<string, IServiceProvider, HttpClient>> ProxyClientActions { get; }

        public NucleusHttpClientBuilderOptions()
        {
            ProxyClientBuildActions = new List<Action<string, IHttpClientBuilder>>();
            ConfiguredProxyClients = new HashSet<string>();
            ProxyClientActions = new List<Action<string, IServiceProvider, HttpClient>>();
        }
    }
}



