using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Nucleus.Aspects;
using Nucleus.DependencyInjection;
using Nucleus.Features;

namespace Nucleus.AspNetCore.Mvc.Features
{
    public class NucleusFeatureActionFilter : IAsyncActionFilter, ITransientDependency
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            using (NucleusCrossCuttingConcerns.Applying(context.Controller, NucleusCrossCuttingConcerns.FeatureChecking))
            {
                var methodInvocationFeatureCheckerService = context.GetRequiredService<IMethodInvocationFeatureCheckerService>();
                await methodInvocationFeatureCheckerService.CheckAsync(new MethodInvocationFeatureCheckerContext(methodInfo));

                await next();
            }
        }
    }
}





