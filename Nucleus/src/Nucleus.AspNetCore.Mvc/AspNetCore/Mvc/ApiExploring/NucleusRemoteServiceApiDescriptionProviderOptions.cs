using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Nucleus.AspNetCore.Mvc.ApiExploring
{
    public class NucleusRemoteServiceApiDescriptionProviderOptions
    {
        public HashSet<ApiResponseType> SupportedResponseTypes { get; set; } = new HashSet<ApiResponseType>();
    }
}

