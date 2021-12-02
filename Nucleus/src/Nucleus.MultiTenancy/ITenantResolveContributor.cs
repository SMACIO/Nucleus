using System.Threading.Tasks;

namespace Nucleus.MultiTenancy
{
    public interface ITenantResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(ITenantResolveContext context);
    }
}

