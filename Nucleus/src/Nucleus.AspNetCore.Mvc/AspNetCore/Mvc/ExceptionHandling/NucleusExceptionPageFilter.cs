using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Nucleus.AspNetCore.ExceptionHandling;
using Nucleus.Authorization;
using Nucleus.DependencyInjection;
using Nucleus.ExceptionHandling;
using Nucleus.Http;
using Nucleus.Json;

namespace Nucleus.AspNetCore.Mvc.ExceptionHandling
{
    public class NucleusExceptionPageFilter : IAsyncPageFilter, ITransientDependency
    {
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerMethod == null || !ShouldHandleException(context))
            {
                await next();
                return;
            }

            var pageHandlerExecutedContext = await next();
            if (pageHandlerExecutedContext.Exception == null)
            {
                return;;
            }

            await HandleAndWrapException(pageHandlerExecutedContext);
        }

        protected virtual bool ShouldHandleException(PageHandlerExecutingContext context)
        {
            //TODO: Create DontWrap attribute to control wrapping..?

            if (context.ActionDescriptor.IsPageAction() &&
                ActionResultHelper.IsObjectResult(context.HandlerMethod.MethodInfo.ReturnType, typeof(void)))
            {
                return true;
            }

            if (context.HttpContext.Request.CanAccept(MimeTypes.Application.Json))
            {
                return true;
            }

            if (context.HttpContext.Request.IsAjax())
            {
                return true;
            }

            return false;
        }

        protected virtual async Task HandleAndWrapException(PageHandlerExecutedContext context)
        {
            //TODO: Trigger an NucleusExceptionHandled event or something like that.

            var exceptionHandlingOptions = context.GetRequiredService<IOptions<NucleusExceptionHandlingOptions>>().Value;
            var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
            var remoteServiceErrorInfo  = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
            {
                options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            });

            var logLevel = context.Exception.GetLogLevel();

            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<NucleusExceptionFilter>>(NullLogger<NucleusExceptionFilter>.Instance);
            logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

            logger.LogException(context.Exception, logLevel);

            await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));

            if (context.Exception is NucleusAuthorizationException)
            {
                await context.HttpContext.RequestServices.GetRequiredService<INucleusAuthorizationExceptionHandler>()
                    .HandleAsync(context.Exception.As<NucleusAuthorizationException>(), context.HttpContext);
            }
            else
            {
                context.HttpContext.Response.Headers.Add(NucleusHttpConsts.NucleusErrorFormat, "true");
                context.HttpContext.Response.StatusCode = (int) context
                    .GetRequiredService<IHttpExceptionStatusCodeFinder>()
                    .GetStatusCode(context.HttpContext, context.Exception);

                context.Result = new ObjectResult(new RemoteServiceErrorResponse(remoteServiceErrorInfo));
            }

            context.Exception = null; //Handled!
        }
    }
}







