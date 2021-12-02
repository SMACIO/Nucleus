using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Authorization.Permissions
{
    public class PermissionValueProviderManager : IPermissionValueProviderManager, ISingletonDependency
    {
        public IReadOnlyList<IPermissionValueProvider> ValueProviders => _lazyProviders.Value;
        private readonly Lazy<List<IPermissionValueProvider>> _lazyProviders;

        protected NucleusPermissionOptions Options { get; }

        public PermissionValueProviderManager(
            IServiceProvider serviceProvider,
            IOptions<NucleusPermissionOptions> options)
        {
            Options = options.Value;

            _lazyProviders = new Lazy<List<IPermissionValueProvider>>(
                () => Options
                    .ValueProviders
                    .Select(c => serviceProvider.GetRequiredService(c) as IPermissionValueProvider)
                    .ToList(),
                true
            );
        }
    }
}



