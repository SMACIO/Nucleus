using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Nucleus.Data
{
    public interface IConnectionStringResolver
    {
        [NotNull]
        [Obsolete("Use ResolveAsync method.")]
        string Resolve(string connectionStringName = null);

        [NotNull]
        Task<string> ResolveAsync(string connectionStringName = null);
    }
}

