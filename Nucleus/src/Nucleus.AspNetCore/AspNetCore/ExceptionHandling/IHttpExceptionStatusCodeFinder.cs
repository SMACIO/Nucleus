using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Nucleus.AspNetCore.ExceptionHandling
{
    public interface IHttpExceptionStatusCodeFinder
    {
        HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception);
    }
}
