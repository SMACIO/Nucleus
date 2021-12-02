using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nucleus.Aspects;
using Nucleus.DependencyInjection;
using Nucleus.GlobalFeatures;

namespace Nucleus.AspNetCore.Mvc.GlobalFeatures
{
    public class GlobalFeatureActionFilter : IAsyncActionFilter, ITransientDependency
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(context.Controller.GetType(), out var attribute))
            {
                var logger = context.GetService<ILogger<GlobalFeatureActionFilter>>(NullLogger<GlobalFeatureActionFilter>.Instance);
                logger.LogWarning($"The '{context.Controller.GetType().FullName}' controller needs to enable '{attribute.Name}' feature.");
                context.Result = new NotFoundResult();
                return;
            }

            using (NucleusCrossCuttingConcerns.Applying(context.Controller, NucleusCrossCuttingConcerns.GlobalFeatureChecking))
            {
                await next();
            }
        }
    }
}




