using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Hosting
{
    public static class NucleusHostingEnvironmentExtensions
    {
        public static IConfigurationRoot BuildConfiguration(
            this IWebHostEnvironment env,
            NucleusConfigurationBuilderOptions options = null)
        {
            options = options ?? new NucleusConfigurationBuilderOptions();

            if (options.BasePath.IsNullOrEmpty())
            {
                options.BasePath = env.ContentRootPath;
            }

            if (options.EnvironmentName.IsNullOrEmpty())
            {
                options.EnvironmentName = env.EnvironmentName;
            }

            return ConfigurationHelper.BuildConfiguration(options);
        }
    }
}



