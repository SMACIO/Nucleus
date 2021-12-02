using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.SecurityLog
{
    public class DefaultSecurityLogManager : ISecurityLogManager, ITransientDependency
    {
        protected NucleusSecurityLogOptions SecurityLogOptions { get; }

        protected ISecurityLogStore SecurityLogStore { get; }

        public DefaultSecurityLogManager(
            IOptions<NucleusSecurityLogOptions> securityLogOptions,
            ISecurityLogStore securityLogStore)
        {
            SecurityLogStore = securityLogStore;
            SecurityLogOptions = securityLogOptions.Value;
        }

        public async Task SaveAsync(Action<SecurityLogInfo> saveAction = null)
        {
            if (!SecurityLogOptions.IsEnabled)
            {
                return;
            }

            var securityLogInfo = await CreateAsync();
            saveAction?.Invoke(securityLogInfo);
            await SecurityLogStore.SaveAsync(securityLogInfo);
        }

        protected virtual Task<SecurityLogInfo> CreateAsync()
        {
            return Task.FromResult(new SecurityLogInfo
            {
                ApplicationName = SecurityLogOptions.ApplicationName
            });
        }
    }
}




