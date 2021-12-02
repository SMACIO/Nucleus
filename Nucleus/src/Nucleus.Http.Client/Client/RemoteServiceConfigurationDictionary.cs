using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.Http.Client
{
    public class RemoteServiceConfigurationDictionary : Dictionary<string, RemoteServiceConfiguration>
    {
        public const string DefaultName = "Default";

        public RemoteServiceConfiguration Default
        {
            get => this.GetOrDefault(DefaultName);
            set => this[DefaultName] = value;
        }

        [NotNull]
        public RemoteServiceConfiguration GetConfigurationOrDefault(string name)
        {
            return this.GetOrDefault(name)
                   ?? Default
                   ?? throw new NucleusException($"Remote service '{name}' was not found and there is no default configuration.");
        }
        
        [CanBeNull]
        public RemoteServiceConfiguration GetConfigurationOrDefaultOrNull(string name)
        {
            return this.GetOrDefault(name)
                   ?? Default;
        }
    }
}

