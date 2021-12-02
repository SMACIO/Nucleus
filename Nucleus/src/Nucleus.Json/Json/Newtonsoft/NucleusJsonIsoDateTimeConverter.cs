using System;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nucleus.DependencyInjection;
using Nucleus.Timing;

namespace Nucleus.Json.Newtonsoft
{
    public class NucleusJsonIsoDateTimeConverter : IsoDateTimeConverter, ITransientDependency
    {
        private readonly IClock _clock;

        public NucleusJsonIsoDateTimeConverter(IClock clock, IOptions<NucleusJsonOptions> nucleusJsonOptions)
        {
            _clock = clock;

            if (nucleusJsonOptions.Value.DefaultDateTimeFormat != null)
            {
                DateTimeFormat = nucleusJsonOptions.Value.DefaultDateTimeFormat;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var date = base.ReadJson(reader, objectType, existingValue, serializer) as DateTime?;

            if (date.HasValue)
            {
                return _clock.Normalize(date.Value);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            base.WriteJson(writer, date.HasValue ? _clock.Normalize(date.Value) : value, serializer);
        }
    }
}






