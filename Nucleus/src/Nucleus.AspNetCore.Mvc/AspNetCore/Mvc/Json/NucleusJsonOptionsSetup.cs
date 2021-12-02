using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.Json.SystemTextJson.JsonConverters;

namespace Nucleus.AspNetCore.Mvc.Json
{
    public class NucleusJsonOptionsSetup : IConfigureOptions<JsonOptions>
    {
        protected IServiceProvider ServiceProvider { get; }

        public NucleusJsonOptionsSetup(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            options.JsonSerializerOptions.AllowTrailingCommas = true;

            options.JsonSerializerOptions.Converters.Add(ServiceProvider.GetRequiredService<NucleusDateTimeConverter>());
            options.JsonSerializerOptions.Converters.Add(ServiceProvider.GetRequiredService<NucleusNullableDateTimeConverter>());

            options.JsonSerializerOptions.Converters.Add(new NucleusStringToEnumFactory());
            options.JsonSerializerOptions.Converters.Add(new NucleusStringToBooleanConverter());

            options.JsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());
            options.JsonSerializerOptions.Converters.Add(new NucleusHasExtraPropertiesJsonConverterFactory());
        }
    }
}






