using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nucleus.Authorization;
using Nucleus.Data;
using Nucleus.DependencyInjection;
using Nucleus.Domain.Entities;
using Nucleus.ExceptionHandling;
using Nucleus.Validation;

namespace Nucleus.AspNetCore.ExceptionHandling
{
    public class DefaultHttpExceptionStatusCodeFinder : IHttpExceptionStatusCodeFinder, ITransientDependency
    {
        protected NucleusExceptionHttpStatusCodeOptions Options { get; }

        public DefaultHttpExceptionStatusCodeFinder(
            IOptions<NucleusExceptionHttpStatusCodeOptions> options)
        {
            Options = options.Value;
        }

        public virtual HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception)
        {
            if (exception is IHasHttpStatusCode exceptionWithHttpStatusCode &&
                exceptionWithHttpStatusCode.HttpStatusCode > 0)
            {
                return (HttpStatusCode) exceptionWithHttpStatusCode.HttpStatusCode;
            }

            if (exception is IHasErrorCode exceptionWithErrorCode &&
                !exceptionWithErrorCode.Code.IsNullOrWhiteSpace())
            {
                if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue(exceptionWithErrorCode.Code, out var status))
                {
                    return status;
                }
            }

            if (exception is NucleusAuthorizationException)
            {
                return httpContext.User.Identity.IsAuthenticated
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            //TODO: Handle SecurityException..?

            if (exception is NucleusValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }
            
            if (exception is NucleusDbConcurrencyException)
            {
                return HttpStatusCode.Conflict;
            }

            if (exception is NotImplementedException)
            {
                return HttpStatusCode.NotImplemented;
            }

            if (exception is IBusinessException)
            {
                return HttpStatusCode.Forbidden;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}




