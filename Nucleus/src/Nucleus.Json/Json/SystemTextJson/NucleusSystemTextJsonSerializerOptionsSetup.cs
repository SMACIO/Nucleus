using System;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.Json.SystemTextJson.JsonConverters;

namespace Nucleus.Json.SystemTextJson
{
    public class NucleusSystemTextJsonSerializerOptionsSetup : IConfigureOptions<NucleusSystemTextJsonSerializerOptions>
    {
        protected IServiceProvider ServiceProvider { get; }

        public NucleusSystemTextJsonSerializerOptionsSetup(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void Configure(NucleusSystemTextJsonSerializerOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(ServiceProvider.GetRequiredService<NucleusDateTimeConverter>());
            options.JsonSerializerOptions.Converters.Add(ServiceProvider.GetRequiredService<NucleusNullableDateTimeConverter>());

            options.JsonSerializerOptions.Converters.Add(new NucleusStringToEnumFactory());
            options.JsonSerializerOptions.Converters.Add(new NucleusStringToBooleanConverter());

            options.JsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());
            options.JsonSerializerOptions.Converters.Add(new NucleusHasExtraPropertiesJsonConverterFactory());

            // If the user hasn't explicitly configured the encoder, use the less strict encoder that does not encode all non-ASCII characters.
            options.JsonSerializerOptions.Encoder ??= JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        }
    }
}







