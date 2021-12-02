using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nucleus.Authorization;

namespace Nucleus.AspNetCore.ExceptionHandling
{
    public interface INucleusAuthorizationExceptionHandler
    {
        Task HandleAsync(NucleusAuthorizationException exception, HttpContext httpContext);
    }
}




