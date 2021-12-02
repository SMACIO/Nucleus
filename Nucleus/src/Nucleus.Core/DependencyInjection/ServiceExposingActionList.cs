using System;
using System.Collections.Generic;

namespace Nucleus.DependencyInjection
{
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {

    }
}
