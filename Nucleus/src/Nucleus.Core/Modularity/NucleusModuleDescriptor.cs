using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using JetBrains.Annotations;

namespace Nucleus.Modularity
{
    public class NucleusModuleDescriptor : INucleusModuleDescriptor
    {
        public Type Type { get; }

        public Assembly Assembly { get; }

        public INucleusModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public IReadOnlyList<INucleusModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
        private readonly List<INucleusModuleDescriptor> _dependencies;

        public NucleusModuleDescriptor(
            [NotNull] Type type, 
            [NotNull] INucleusModule instance, 
            bool isLoadedAsPlugIn)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
            }

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;

            _dependencies = new List<INucleusModuleDescriptor>();
        }

        public void AddDependency(INucleusModuleDescriptor descriptor)
        {
            _dependencies.AddIfNotContains(descriptor);
        }

        public override string ToString()
        {
            return $"[NucleusModuleDescriptor {Type.FullName}]";
        }
    }
}







