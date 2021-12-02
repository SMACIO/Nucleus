using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Nucleus.Json.Newtonsoft;
using Nucleus.Json.SystemTextJson;
using Nucleus.Modularity;
using Nucleus.Timing;

namespace Nucleus.Json
{
    [DependsOn(typeof(NucleusTimingModule))]
    public class NucleusJsonModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddEnumerable(ServiceDescriptor
                .Transient<IConfigureOptions<NucleusSystemTextJsonSerializerOptions>, NucleusSystemTextJsonSerializerOptionsSetup>());

            Configure<NucleusJsonOptions>(options =>
            {
                options.Providers.Add<NucleusNewtonsoftJsonSerializerProvider>();

                var nucleusJsonOptions = context.Services.ExecutePreConfiguredActions<NucleusJsonOptions>();
                if (nucleusJsonOptions.UseHybridSerializer)
                {
                    options.Providers.Add<NucleusSystemTextJsonSerializerProvider>();
                }
            });

            Configure<NucleusNewtonsoftJsonSerializerOptions>(options =>
            {
                options.Converters.Add<NucleusJsonIsoDateTimeConverter>();
            });
        }
    }
}









