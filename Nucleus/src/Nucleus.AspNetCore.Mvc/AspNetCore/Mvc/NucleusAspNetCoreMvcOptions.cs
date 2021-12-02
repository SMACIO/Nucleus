using System;
using System.Collections.Generic;
using Nucleus.AspNetCore.Mvc.Conventions;

namespace Nucleus.AspNetCore.Mvc
{
    public class NucleusAspNetCoreMvcOptions
    {
        public bool? MinifyGeneratedScript { get; set; }

        public NucleusConventionalControllerOptions ConventionalControllers { get; }

        public HashSet<Type> IgnoredControllersOnModelExclusion { get; }

        public bool AutoModelValidation { get; set; }

        public NucleusAspNetCoreMvcOptions()
        {
            ConventionalControllers = new NucleusConventionalControllerOptions();
            IgnoredControllersOnModelExclusion = new HashSet<Type>();
            AutoModelValidation = true;
        }
    }
}





