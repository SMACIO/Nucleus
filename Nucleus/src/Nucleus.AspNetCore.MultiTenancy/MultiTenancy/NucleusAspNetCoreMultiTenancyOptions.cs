using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nucleus.MultiTenancy;

namespace Nucleus.AspNetCore.MultiTenancy
{
    public class NucleusAspNetCoreMultiTenancyOptions
    {
        /// <summary>
        /// Default: <see cref="TenantResolverConsts.DefaultTenantKey"/>.
        /// </summary>
        public string TenantKey { get; set; }

        public Func<HttpContext, Exception, Task> MultiTenancyMiddlewareErrorPageBuilder { get; set; }

        public NucleusAspNetCoreMultiTenancyOptions()
        {
            TenantKey = TenantResolverConsts.DefaultTenantKey;
            MultiTenancyMiddlewareErrorPageBuilder = async (context, exception)=>
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;;
                context.Response.ContentType = "text/html";

                var message = exception.Message;
                var details = exception is BusinessException businessException ? businessException.Details : string.Empty;

                await context.Response.WriteAsync($"<html lang=\"{CultureInfo.CurrentCulture.Name}\"><body>\r\n");
                await context.Response.WriteAsync($"<h3>{message}</h3>{details}<br>\r\n");
                await context.Response.WriteAsync("</body></html>\r\n");

                // Note the 500 spaces are to work around an IE 'feature'
                await context.Response.WriteAsync(new string(' ', 500));
            };
        }
    }
}




