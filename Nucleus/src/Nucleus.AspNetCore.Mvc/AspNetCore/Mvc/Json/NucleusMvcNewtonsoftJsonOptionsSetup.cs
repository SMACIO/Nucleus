using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nucleus.AspNetCore.Mvc.Json
{
    public class NucleusMvcNewtonsoftJsonOptionsSetup : IConfigureOptions<MvcNewtonsoftJsonOptions>
    {
        protected IServiceProvider ServiceProvider { get; }

        public NucleusMvcNewtonsoftJsonOptionsSetup(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void Configure(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = ServiceProvider.GetRequiredService<NucleusMvcJsonContractResolver>();
        }
    }
}




