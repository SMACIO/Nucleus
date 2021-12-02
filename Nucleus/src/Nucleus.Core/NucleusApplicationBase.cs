using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nucleus.DependencyInjection;
using Nucleus.Internal;
using Nucleus.Logging;
using Nucleus.Modularity;

namespace Nucleus
{
    public abstract class NucleusApplicationBase : INucleusApplication
    {
        [NotNull]
        public Type StartupModuleType { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IServiceCollection Services { get; }

        public IReadOnlyList<INucleusModuleDescriptor> Modules { get; }

        internal NucleusApplicationBase(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction)
        {
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(services, nameof(services));

            StartupModuleType = startupModuleType;
            Services = services;

            services.TryAddObjectAccessor<IServiceProvider>();

            var options = new NucleusApplicationCreationOptions(services);
            optionsAction?.Invoke(options);

            services.AddSingleton<INucleusApplication>(this);
            services.AddSingleton<IModuleContainer>(this);

            services.AddCoreServices();
            services.AddCoreNucleusServices(this, options);

            Modules = LoadModules(services, options);
            ConfigureServices();
        }

        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        public virtual void Dispose()
        {
            //TODO: Shutdown if not done before?
        }

        protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
        }

        protected virtual void InitializeModules()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                WriteInitLogs(scope.ServiceProvider);
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        protected virtual void WriteInitLogs(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<NucleusApplicationBase>>();
            if (logger == null)
            {
                return;
            }

            var initLogger = serviceProvider.GetRequiredService<IInitLoggerFactory>().Create<NucleusApplicationBase>();

            foreach (var entry in initLogger.Entries)
            {
                logger.Log(entry.LogLevel, entry.EventId, entry.State, entry.Exception, entry.Formatter);
            }

            initLogger.Entries.Clear();
        }

        protected virtual IReadOnlyList<INucleusModuleDescriptor> LoadModules(IServiceCollection services, NucleusApplicationCreationOptions options)
        {
            return services
                .GetSingletonInstance<IModuleLoader>()
                .LoadModules(
                    services,
                    StartupModuleType,
                    options.PlugInSources
                );
        }

        //TODO: We can extract a new class for this
        protected virtual void ConfigureServices()
        {
            var context = new ServiceConfigurationContext(Services);
            Services.AddSingleton(context);

            foreach (var module in Modules)
            {
                if (module.Instance is NucleusModule nucleusModule)
                {
                    nucleusModule.ServiceConfigurationContext = context;
                }
            }

            //PreConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
            {
                try
                {
                    ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new NucleusInitializationException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            var assemblies = new HashSet<Assembly>();

            //ConfigureServices
            foreach (var module in Modules)
            {
                if (module.Instance is NucleusModule nucleusModule)
                {
                    if (!nucleusModule.SkipAutoServiceRegistration)
                    {
                        var assembly = module.Type.Assembly;
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                }

                try
                {
                    module.Instance.ConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new NucleusInitializationException($"An error occurred during {nameof(INucleusModule.ConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            //PostConfigureServices
            foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
            {
                try
                {
                    ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new NucleusInitializationException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase of the module {module.Type.AssemblyQualifiedName}. See the inner exception for details.", ex);
                }
            }

            foreach (var module in Modules)
            {
                if (module.Instance is NucleusModule nucleusModule)
                {
                    nucleusModule.ServiceConfigurationContext = null;
                }
            }
        }
    }
}












