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
    public class GlobalFeaturePageFilter: IAsyncPageFilter, ITransientDependency
    {
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerInstance == null || !context.ActionDescriptor.IsPageAction())
            {
                await next();
                return;
            }

            if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(context.HandlerInstance.GetType(), out var attribute))
            {
                var logger = context.GetService<ILogger<GlobalFeatureActionFilter>>(NullLogger<GlobalFeatureActionFilter>.Instance);
                logger.LogWarning($"The '{context.HandlerInstance.GetType().FullName}' page needs to enable '{attribute.Name}' feature.");
                context.Result = new NotFoundResult();
                return;
            }

            using (NucleusCrossCuttingConcerns.Applying(context.HandlerInstance, NucleusCrossCuttingConcerns.GlobalFeatureChecking))
            {
                await next();
            }
        }
    }
}




