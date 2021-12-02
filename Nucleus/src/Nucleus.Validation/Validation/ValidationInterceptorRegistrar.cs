﻿using System;
using Nucleus.DependencyInjection;
using Nucleus.DynamicProxy;

namespace Nucleus.Validation
{
    public static class ValidationInterceptorRegistrar
    {
        public static void RegisterIfNeeded(IOnServiceRegistredContext context)
        {
            if (ShouldIntercept(context.ImplementationType))
            {
                context.Interceptors.TryAdd<ValidationInterceptor>();
            }
        }
        
         private static bool ShouldIntercept(Type type)
         {
             return !DynamicProxyIgnoreTypes.Contains(type) && typeof(IValidationEnabled).IsAssignableFrom(type);
         }
    }
}

