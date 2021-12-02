using System;

namespace Nucleus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending
{
    public interface IExtensionPropertyAttributeDtoFactory
    {
        ExtensionPropertyAttributeDto Create(Attribute attribute);
    }
}
