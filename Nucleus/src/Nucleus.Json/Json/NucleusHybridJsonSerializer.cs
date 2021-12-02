using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Json
{
    public class NucleusHybridJsonSerializer : IJsonSerializer, ITransientDependency
    {
        protected NucleusJsonOptions Options { get; }

        protected IServiceScopeFactory ServiceScopeFactory { get; }

        public NucleusHybridJsonSerializer(IOptions<NucleusJsonOptions> options, IServiceScopeFactory serviceScopeFactory)
        {
            Options = options.Value;
            ServiceScopeFactory = serviceScopeFactory;
        }

        public string Serialize([CanBeNull]object obj, bool camelCase = true, bool indented = false)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var serializerProvider = GetSerializerProvider(scope.ServiceProvider, obj?.GetType());
                return serializerProvider.Serialize(obj, camelCase, indented);
            }
        }

        public T Deserialize<T>([NotNull]string jsonString, bool camelCase = true)
        {
            Check.NotNull(jsonString, nameof(jsonString));

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var serializerProvider = GetSerializerProvider(scope.ServiceProvider, typeof(T));
                return serializerProvider.Deserialize<T>(jsonString, camelCase);
            }
        }

        public object Deserialize(Type type, [NotNull]string jsonString, bool camelCase = true)
        {
            Check.NotNull(jsonString, nameof(jsonString));

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var serializerProvider = GetSerializerProvider(scope.ServiceProvider, type);
                return serializerProvider.Deserialize(type, jsonString, camelCase);
            }
        }

        protected virtual IJsonSerializerProvider GetSerializerProvider(IServiceProvider serviceProvider, [CanBeNull]Type type)
        {
            foreach (var providerType in Options.Providers.Reverse())
            {
                var provider = serviceProvider.GetRequiredService(providerType) as IJsonSerializerProvider;
                if (provider.CanHandle(type))
                {
                    return provider;
                }
            }

            throw new NucleusException($"There is no IJsonSerializerProvider that can handle '{type.GetFullNameWithAssemblyName()}'!");
        }
    }
}






