﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nucleus.Json.SystemTextJson.JsonConverters
{
    public class NucleusStringToEnumConverter<T> : JsonConverter<T>
        where T : struct, Enum
    {
        private readonly JsonStringEnumConverter _innerJsonStringEnumConverter;

        public NucleusStringToEnumConverter()
            : this(namingPolicy: null, allowIntegerValues: true)
        {

        }

        public NucleusStringToEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
        {
            _innerJsonStringEnumConverter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var newOptions = JsonSerializerOptionsHelper.Create(options, x =>
                x == this ||
                x.GetType() == typeof(NucleusStringToEnumFactory));

            newOptions.Converters.Add(_innerJsonStringEnumConverter.CreateConverter(typeToConvert, newOptions));
            return JsonSerializer.Deserialize<T>(ref reader, newOptions);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var newOptions = JsonSerializerOptionsHelper.Create(options, x =>
                x == this ||
                x.GetType() == typeof(NucleusStringToEnumFactory));

            JsonSerializer.Serialize(writer, value, newOptions);
        }
    }
}




