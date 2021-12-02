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
    public class NucleusExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!ShouldHandleException(context))
            {
                return;
            }

            await HandleAndWrapException(context);
        }

        protected virtual bool ShouldHandleException(ExceptionContext context)
        {
            //TODO: Create DontWrap attribute to control wrapping..?

            if (context.ActionDescriptor.IsControllerAction() &&
                context.ActionDescriptor.HasObjectResult())
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

        protected virtual async Task HandleAndWrapException(ExceptionContext context)
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







