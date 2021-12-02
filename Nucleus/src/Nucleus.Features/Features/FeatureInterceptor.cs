using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Aspects;
using Nucleus.DependencyInjection;
using Nucleus.DynamicProxy;

namespace Nucleus.Features
{
    public class FeatureInterceptor : NucleusInterceptor, ITransientDependency
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FeatureInterceptor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task InterceptAsync(INucleusMethodInvocation invocation)
        {
            if (NucleusCrossCuttingConcerns.IsApplied(invocation.TargetObject, NucleusCrossCuttingConcerns.FeatureChecking))
            {
                await invocation.ProceedAsync();
                return;
            }

            await CheckFeaturesAsync(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual async Task CheckFeaturesAsync(INucleusMethodInvocation invocation)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<IMethodInvocationFeatureCheckerService>().CheckAsync(
                    new MethodInvocationFeatureCheckerContext(
                        invocation.Method
                    )
                );
            }
        }
    }
}





