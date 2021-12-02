using System;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Json.SystemTextJson
{
    public class NucleusSystemTextJsonSerializerProvider : IJsonSerializerProvider, ITransientDependency
    {
        protected NucleusSystemTextJsonSerializerOptions Options { get; }

        protected NucleusSystemTextJsonUnsupportedTypeMatcher NucleusSystemTextJsonUnsupportedTypeMatcher { get; }

        public NucleusSystemTextJsonSerializerProvider(
            IOptions<NucleusSystemTextJsonSerializerOptions> options,
            NucleusSystemTextJsonUnsupportedTypeMatcher nucleusSystemTextJsonUnsupportedTypeMatcher)
        {
            NucleusSystemTextJsonUnsupportedTypeMatcher = nucleusSystemTextJsonUnsupportedTypeMatcher;
            Options = options.Value;
        }

        public bool CanHandle(Type type)
        {
            return !NucleusSystemTextJsonUnsupportedTypeMatcher.Match(type);
        }

        public string Serialize(object obj, bool camelCase = true, bool indented = false)
        {
            return JsonSerializer.Serialize(obj, CreateJsonSerializerOptions(camelCase, indented));
        }

        public T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize<T>(jsonString, CreateJsonSerializerOptions(camelCase));
        }

        public object Deserialize(Type type, string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize(jsonString, type, CreateJsonSerializerOptions(camelCase));
        }

        protected virtual JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = true, bool indented = false)
        {
            var settings = new JsonSerializerOptions(Options.JsonSerializerOptions);

            if (camelCase)
            {
                settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }

            if (indented)
            {
                settings.WriteIndented = true;
            }

            return settings;
        }
    }
}







