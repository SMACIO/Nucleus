using System.Collections.Generic;

namespace Nucleus.AspNetCore.Auditing
{
    public class NucleusAspNetCoreAuditingOptions
    {
        /// <summary>
        /// This is used to disable the <see cref="NucleusAuditingMiddleware"/>,
        /// app.UseAuditing(), for the specified URLs.
        /// <see cref="NucleusAuditingMiddleware"/> will be disabled for URLs
        /// starting with an ignored URL.  
        /// </summary>
        public List<string> IgnoredUrls { get; } = new List<string>();
    }
}


