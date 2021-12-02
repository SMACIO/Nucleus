using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nucleus.DependencyInjection;
using Nucleus.Json.Newtonsoft;
using Nucleus.Reflection;
using Nucleus.Timing;

namespace Nucleus.AspNetCore.Mvc.Json
{
    public class NucleusMvcJsonContractResolver : DefaultContractResolver, ITransientDependency
    {
        private readonly Lazy<NucleusJsonIsoDateTimeConverter> _dateTimeConverter;

        public NucleusMvcJsonContractResolver(IServiceProvider serviceProvider)
        {
            _dateTimeConverter = new Lazy<NucleusJsonIsoDateTimeConverter>(
                serviceProvider.GetRequiredService<NucleusJsonIsoDateTimeConverter>,
                true
            );

            NamingStrategy = new CamelCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null)
            {
                property.Converter = _dateTimeConverter.Value;
            }
        }
    }
}





