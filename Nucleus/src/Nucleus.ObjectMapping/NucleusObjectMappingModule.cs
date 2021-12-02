using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;
using Nucleus.Reflection;

namespace Nucleus.ObjectMapping
{
    public class NucleusObjectMappingModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnExposing(onServiceExposingContext =>
            {
                //Register types for IObjectMapper<TSource, TDestination> if implements
                onServiceExposingContext.ExposedTypes.AddRange(
                    ReflectionHelper.GetImplementedGenericTypes(
                        onServiceExposingContext.ImplementationType,
                        typeof(IObjectMapper<,>)
                    )
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IObjectMapper<>),
                typeof(DefaultObjectMapper<>)
            );
        }
    }
}




