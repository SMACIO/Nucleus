using System;
using System.Reflection;

namespace Nucleus.Http.ProxyScripting.Configuration
{
    public static class NucleusApiProxyScriptingConfiguration
    {
        public static Func<PropertyInfo, string> PropertyNameGenerator { get; set; }
        
        static NucleusApiProxyScriptingConfiguration()
        {
            PropertyNameGenerator = propertyInfo =>
            {
                var jsonPropertyNameAttribute = propertyInfo.GetSingleAttributeOrNull<System.Text.Json.Serialization.JsonPropertyNameAttribute>(true);

                if (jsonPropertyNameAttribute != null)
                {
                    return jsonPropertyNameAttribute.Name;
                }
                
                var jsonPropertyAttribute = propertyInfo.GetSingleAttributeOrNull<Newtonsoft.Json.JsonPropertyAttribute>(true);

                if (jsonPropertyAttribute != null)
                {
                    return jsonPropertyAttribute.PropertyName;
                }

                return null;
            };
        }
    }
}


