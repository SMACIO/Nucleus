using System;
using System.Collections.Generic;

namespace Nucleus.Http.Client.ClientProxying
{
    public class NucleusHttpClientProxyingOptions
    {
        public Dictionary<Type, Type> QueryStringConverts { get; set; }

        public Dictionary<Type, Type> FormDataConverts { get; set; }

        public NucleusHttpClientProxyingOptions()
        {
            QueryStringConverts = new Dictionary<Type, Type>();
            FormDataConverts = new Dictionary<Type, Type>();
        }
    }
}



