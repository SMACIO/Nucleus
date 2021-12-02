using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nucleus.ApiVersioning;

namespace Nucleus.AspNetCore.Mvc.Versioning
{
    public class HttpContextRequestedApiVersion : IRequestedApiVersion
    {
        public string Current => _httpContextAccessor.HttpContext?.GetRequestedApiVersion().ToString();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextRequestedApiVersion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

