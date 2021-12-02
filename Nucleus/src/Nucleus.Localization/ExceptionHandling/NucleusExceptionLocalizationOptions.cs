using System;
using System.Collections.Generic;

namespace Nucleus.Localization.ExceptionHandling
{
    public class NucleusExceptionLocalizationOptions
    {
        public Dictionary<string, Type> ErrorCodeNamespaceMappings { get; }

        public NucleusExceptionLocalizationOptions()
        {
            ErrorCodeNamespaceMappings = new Dictionary<string, Type>();
        }

        public void MapCodeNamespace(string errorCodeNamespace, Type type)
        {
            ErrorCodeNamespaceMappings[errorCodeNamespace] = type;
        }
    }
}



