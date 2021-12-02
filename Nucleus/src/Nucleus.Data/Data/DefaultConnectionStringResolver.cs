using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Data
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        protected NucleusDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver(
            IOptionsMonitor<NucleusDbConnectionOptions> options)
        {
            Options = options.CurrentValue;
        }

        [Obsolete("Use ResolveAsync method.")]
        public virtual string Resolve(string connectionStringName = null)
        {
            return ResolveInternal(connectionStringName);
        }

        public virtual Task<string> ResolveAsync(string connectionStringName = null)
        {
            return Task.FromResult(ResolveInternal(connectionStringName));
        }

        private string ResolveInternal(string connectionStringName)
        {
            if (connectionStringName == null)
            {
                return Options.ConnectionStrings.Default;
            }

            var connectionString = Options.GetConnectionStringOrNull(connectionStringName);

            if (!connectionString.IsNullOrEmpty())
            {
                return connectionString;
            }

            return null;
        }
    }
}




