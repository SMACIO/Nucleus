using System;
using JetBrains.Annotations;
using Nucleus.Data;

namespace Nucleus.MultiTenancy
{
    [Serializable]
    public class TenantConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }

        public bool IsActive { get; set; }

        public TenantConfiguration()
        {
            IsActive = true;
        }

        public TenantConfiguration(Guid id, [NotNull] string name)
            : this()
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;

            ConnectionStrings = new ConnectionStrings();
        }
    }
}


