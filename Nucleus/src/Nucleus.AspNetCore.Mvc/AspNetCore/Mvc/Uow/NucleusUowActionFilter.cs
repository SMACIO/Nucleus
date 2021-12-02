using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;
using Nucleus.Uow;

namespace Nucleus.AspNetCore.Mvc.Uow
{
    public class NucleusUowActionFilter : IAsyncActionFilter, ITransientDependency
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();
            var unitOfWorkAttr = UnitOfWorkHelper.GetUnitOfWorkAttributeOrNull(methodInfo);

            context.HttpContext.Items["_NucleusActionInfo"] = new NucleusActionInfoInHttpContext
            {
                IsObjectResult = context.ActionDescriptor.HasObjectResult()
            };

            if (unitOfWorkAttr?.IsDisabled == true)
            {
                await next();
                return;
            }

            var options = CreateOptions(context, unitOfWorkAttr);

            var unitOfWorkManager = context.GetRequiredService<IUnitOfWorkManager>();

            //Trying to begin a reserved UOW by NucleusUnitOfWorkMiddleware
            if (unitOfWorkManager.TryBeginReserved(UnitOfWork.UnitOfWorkReservationName, options))
            {
                var result = await next();
                if (Succeed(result))
                {
                    await SaveChangesAsync(context, unitOfWorkManager);
                }
                else
                {
                    await RollbackAsync(context, unitOfWorkManager);
                }

                return;
            }

            using (var uow = unitOfWorkManager.Begin(options))
            {
                var result = await next();
                if (Succeed(result))
                {
                    await uow.CompleteAsync(context.HttpContext.RequestAborted);
                }
                else
                {
                    await uow.RollbackAsync(context.HttpContext.RequestAborted);
                }
            }
        }

        private NucleusUnitOfWorkOptions CreateOptions(ActionExecutingContext context, UnitOfWorkAttribute unitOfWorkAttribute)
        {
            var options = new NucleusUnitOfWorkOptions();

            unitOfWorkAttribute?.SetOptions(options);

            if (unitOfWorkAttribute?.IsTransactional == null)
            {
                var nucleusUnitOfWorkDefaultOptions = context.GetRequiredService<IOptions<NucleusUnitOfWorkDefaultOptions>>().Value;
                options.IsTransactional = nucleusUnitOfWorkDefaultOptions.CalculateIsTransactional(
                    autoValue: !string.Equals(context.HttpContext.Request.Method, HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
                );
            }

            return options;
        }

        private async Task RollbackAsync(ActionExecutingContext context, IUnitOfWorkManager unitOfWorkManager)
        {
            var currentUow = unitOfWorkManager.Current;
            if (currentUow != null)
            {
                await currentUow.RollbackAsync(context.HttpContext.RequestAborted);
            }
        }
        
        private async Task SaveChangesAsync(ActionExecutingContext context, IUnitOfWorkManager unitOfWorkManager)
        {
            var currentUow = unitOfWorkManager.Current;
            if (currentUow != null)
            {
                await currentUow.SaveChangesAsync(context.HttpContext.RequestAborted);
            }
        }

        private static bool Succeed(ActionExecutedContext result)
        {
            return result.Exception == null || result.ExceptionHandled;
        }
    }
}








