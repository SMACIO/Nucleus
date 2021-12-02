using System;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending
{
    [Serializable]
    public class ExtensionEnumFieldDto
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }
}
