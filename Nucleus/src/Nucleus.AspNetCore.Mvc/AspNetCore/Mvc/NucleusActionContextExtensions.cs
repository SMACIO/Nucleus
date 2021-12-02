﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.AspNetCore.Mvc
{
    public static class NucleusActionContextExtensions
    {
        public static T GetRequiredService<T>(this FilterContext context)
            where T : class
        {
            return context.HttpContext.RequestServices.GetRequiredService<T>();
        }

        public static T GetService<T>(this FilterContext context, T defaultValue = default)
            where T : class
        {
            return context.HttpContext.RequestServices.GetService<T>() ?? defaultValue;
        }
    }
}


