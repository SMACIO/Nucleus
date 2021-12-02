using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.DependencyInjection;

namespace Nucleus.AspNetCore.Mvc.Conventions
{
    [DisableConventionalRegistration]
    public class NucleusServiceConventionWrapper : IApplicationModelConvention
    {
        private readonly Lazy<INucleusServiceConvention> _convention;

        public NucleusServiceConventionWrapper(IServiceCollection services)
        {
            _convention = services.GetRequiredServiceLazy<INucleusServiceConvention>();
        }

        public void Apply(ApplicationModel application)
        {
            _convention.Value.Apply(application);
        }
    }
}




