using System.Threading.Tasks;
using Nucleus.Aspects;
using Nucleus.DependencyInjection;
using Nucleus.DynamicProxy;

namespace Nucleus.GlobalFeatures
{
    public class GlobalFeatureInterceptor : NucleusInterceptor, ITransientDependency
    {
        public override async Task InterceptAsync(INucleusMethodInvocation invocation)
        {
            if (NucleusCrossCuttingConcerns.IsApplied(invocation.TargetObject, NucleusCrossCuttingConcerns.GlobalFeatureChecking))
            {
                await invocation.ProceedAsync();
                return;
            }

            if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(invocation.TargetObject.GetType(), out var attribute))
            {
                throw new NucleusGlobalFeatureNotEnabledException(code: NucleusGlobalFeatureErrorCodes.GlobalFeatureIsNotEnabled)
                    .WithData("ServiceName", invocation.TargetObject.GetType().FullName)
                    .WithData("GlobalFeatureName", attribute.Name);
            }

            await invocation.ProceedAsync();
        }
    }
}






