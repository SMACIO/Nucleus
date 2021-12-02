namespace Nucleus.MultiTenancy.ConfigurationStore
{
    public class NucleusDefaultTenantStoreOptions
    {
        public TenantConfiguration[] Tenants { get; set; }

        public NucleusDefaultTenantStoreOptions()
        {
            Tenants = new TenantConfiguration[0];
        }
    }
}


