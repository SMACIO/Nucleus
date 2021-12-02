using System.Reflection;

namespace Nucleus.Authorization
{
    public class MethodInvocationAuthorizationContext
    {
        public MethodInfo Method { get; }

        public MethodInvocationAuthorizationContext(MethodInfo method)
        {
            Method = method;
        }
    }
}
