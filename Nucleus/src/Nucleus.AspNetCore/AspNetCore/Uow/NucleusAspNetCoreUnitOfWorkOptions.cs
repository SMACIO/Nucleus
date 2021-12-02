using System.Collections.Generic;

namespace Nucleus.AspNetCore.Uow
{
    public class NucleusAspNetCoreUnitOfWorkOptions
    {
        /// <summary>
        /// This is used to disable the <see cref="NucleusUnitOfWorkMiddleware"/>,
        /// app.UseUnitOfWork(), for the specified URLs.
        /// <see cref="NucleusUnitOfWorkMiddleware"/> will be disabled for URLs
        /// starting with an ignored URL.  
        /// </summary>
        public List<string> IgnoredUrls { get; } = new List<string>();
    }
}


