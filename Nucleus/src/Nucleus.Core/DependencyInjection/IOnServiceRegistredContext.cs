using System;
using Nucleus.Collections;
using Nucleus.DynamicProxy;

namespace Nucleus.DependencyInjection
{
    public interface IOnServiceRegistredContext
    {
        ITypeList<INucleusInterceptor> Interceptors { get; }

        Type ImplementationType { get; }
    }
}


