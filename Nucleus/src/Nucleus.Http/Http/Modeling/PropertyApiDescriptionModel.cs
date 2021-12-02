using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Nucleus.Http.ProxyScripting.Configuration;

namespace Nucleus.Http.Modeling
{
    [Serializable]
    public class PropertyApiDescriptionModel
    {
        public string Name { get; set; }

        public string JsonName { get; set; }
        
        public string Type { get; set; }

        public string TypeSimple { get; set; }

        public bool IsRequired { get; set; }

        //TODO: Validation rules for this property
        public static PropertyApiDescriptionModel Create(PropertyInfo propertyInfo)
        {
            return new PropertyApiDescriptionModel
            {
                Name = propertyInfo.Name,
                JsonName = NucleusApiProxyScriptingConfiguration.PropertyNameGenerator.Invoke(propertyInfo),
                Type = ApiTypeNameHelper.GetTypeName(propertyInfo.PropertyType),
                TypeSimple = ApiTypeNameHelper.GetSimpleTypeName(propertyInfo.PropertyType),
                IsRequired = propertyInfo.IsDefined(typeof(RequiredAttribute), true)
            };
        }
    }
}



