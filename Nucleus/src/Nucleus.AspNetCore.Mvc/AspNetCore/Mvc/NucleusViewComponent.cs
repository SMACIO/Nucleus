using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.DependencyInjection;
using Nucleus.ObjectMapping;

namespace Nucleus.AspNetCore.Mvc
{
    public abstract class NucleusViewComponent : ViewComponent
    {
        public INucleusLazyServiceProvider LazyServiceProvider { get; set; }

        [Obsolete("Use LazyServiceProvider instead.")]
        public IServiceProvider ServiceProvider { get; set; }

        protected Type ObjectMapperContext { get; set; }

        protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
            ObjectMapperContext == null
                ? provider.GetRequiredService<IObjectMapper>()
                : (IObjectMapper) provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));
    }
}




