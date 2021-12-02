using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;
using Nucleus.Reflection;

namespace Nucleus.Serialization
{
    public class NucleusSerializationModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnExposing(onServiceExposingContext =>
            {
                //Register types for IObjectSerializer<T> if implements
                onServiceExposingContext.ExposedTypes.AddRange(
                    ReflectionHelper.GetImplementedGenericTypes(
                        onServiceExposingContext.ImplementationType,
                        typeof(IObjectSerializer<>)
                    )
                );
            });
        }
    }
}




