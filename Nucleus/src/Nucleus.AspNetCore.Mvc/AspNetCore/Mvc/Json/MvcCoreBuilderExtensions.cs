using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.ObjectPool;
using Nucleus.Json;

namespace Nucleus.AspNetCore.Mvc.Json
{
    public static class MvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddNucleusHybridJson(this IMvcCoreBuilder builder)
        {
            var nucleusJsonOptions = builder.Services.ExecutePreConfiguredActions<NucleusJsonOptions>();
            if (!nucleusJsonOptions.UseHybridSerializer)
            {
                builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcNewtonsoftJsonOptions>, NucleusMvcNewtonsoftJsonOptionsSetup>());
                builder.AddNewtonsoftJson();
                return builder;
            }

            builder.Services.TryAddTransient<DefaultObjectPoolProvider>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<JsonOptions>, NucleusJsonOptionsSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcNewtonsoftJsonOptions>, NucleusMvcNewtonsoftJsonOptionsSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, NucleusHybridJsonOptionsSetup>());
            return builder;
        }
    }
}







