using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.Localization
{
    public class LocalizationResourceDictionary : Dictionary<Type, LocalizationResource>
    {
        public LocalizationResource Add<TResouce>([CanBeNull] string defaultCultureName = null)
        {
            return Add(typeof(TResouce), defaultCultureName);
        }

        public LocalizationResource Add(Type resourceType, [CanBeNull] string defaultCultureName = null)
        {
            if (ContainsKey(resourceType))
            {
                throw new NucleusException("This resource is already added before: " + resourceType.AssemblyQualifiedName);
            }

            return this[resourceType] = new LocalizationResource(resourceType, defaultCultureName);
        }

        public LocalizationResource Get<TResource>()
        {
            var resourceType = typeof(TResource);

            var resource = this.GetOrDefault(resourceType);
            if (resource == null)
            {
                throw new NucleusException("Can not find a resource with given type: " + resourceType.AssemblyQualifiedName);
            }

            return resource;
        }
    }
}

