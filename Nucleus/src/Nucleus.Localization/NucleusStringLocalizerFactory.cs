using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Nucleus.Localization
{
    public class NucleusStringLocalizerFactory : IStringLocalizerFactory, INucleusStringLocalizerFactoryWithDefaultResourceSupport
    {
        protected internal NucleusLocalizationOptions NucleusLocalizationOptions { get; }
        protected ResourceManagerStringLocalizerFactory InnerFactory { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected ConcurrentDictionary<Type, StringLocalizerCacheItem> LocalizerCache { get; }

        //TODO: It's better to use decorator pattern for IStringLocalizerFactory instead of getting ResourceManagerStringLocalizerFactory as a dependency.
        public NucleusStringLocalizerFactory(
            ResourceManagerStringLocalizerFactory innerFactory,
            IOptions<NucleusLocalizationOptions> nucleusLocalizationOptions,
            IServiceProvider serviceProvider)
        {
            InnerFactory = innerFactory;
            ServiceProvider = serviceProvider;
            NucleusLocalizationOptions = nucleusLocalizationOptions.Value;

            LocalizerCache = new ConcurrentDictionary<Type, StringLocalizerCacheItem>();
        }

        public virtual IStringLocalizer Create(Type resourceType)
        {
            var resource = NucleusLocalizationOptions.Resources.GetOrDefault(resourceType);
            if (resource == null)
            {
                return InnerFactory.Create(resourceType);
            }

            if (LocalizerCache.TryGetValue(resourceType, out var cacheItem))
            {
                return cacheItem.Localizer;
            }

            lock (LocalizerCache)
            {
                return LocalizerCache.GetOrAdd(
                    resourceType,
                    _ => CreateStringLocalizerCacheItem(resource)
                ).Localizer;
            }
        }

        private StringLocalizerCacheItem CreateStringLocalizerCacheItem(LocalizationResource resource)
        {
            foreach (var globalContributor in NucleusLocalizationOptions.GlobalContributors)
            {
                resource.Contributors.Add((ILocalizationResourceContributor) Activator.CreateInstance(globalContributor));
            }

            var context = new LocalizationResourceInitializationContext(resource, ServiceProvider);

            foreach (var contributor in resource.Contributors)
            {
                contributor.Initialize(context);
            }

            return new StringLocalizerCacheItem(
                new NucleusDictionaryBasedStringLocalizer(
                    resource,
                    resource.BaseResourceTypes.Select(Create).ToList()
                )
            );
        }

        public virtual IStringLocalizer Create(string baseName, string location)
        {
            //TODO: Investigate when this is called?

            return InnerFactory.Create(baseName, location);
        }

        internal static void Replace(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, NucleusStringLocalizerFactory>());
            services.AddSingleton<ResourceManagerStringLocalizerFactory>();
        }

        protected class StringLocalizerCacheItem
        {
            public NucleusDictionaryBasedStringLocalizer Localizer { get; }

            public StringLocalizerCacheItem(NucleusDictionaryBasedStringLocalizer localizer)
            {
                Localizer = localizer;
            }
        }

        public IStringLocalizer CreateDefaultOrNull()
        {
            if (NucleusLocalizationOptions.DefaultResourceType == null)
            {
                return null;
            }

            return Create(NucleusLocalizationOptions.DefaultResourceType);
        }
    }
}









