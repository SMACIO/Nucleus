using System.Reflection;

namespace Nucleus.Features
{
    public class MethodInvocationFeatureCheckerContext
    {
        public MethodInfo Method { get; }

        public MethodInvocationFeatureCheckerContext(MethodInfo method)
        {
            Method = method;
        }
    }
}
