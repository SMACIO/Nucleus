using System;

namespace Nucleus.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ReplaceDbContextAttribute : Attribute
    {
        public Type[] ReplacedDbContextTypes { get; }

        public ReplaceDbContextAttribute(params Type[] replacedDbContextTypes)
        {
            ReplacedDbContextTypes = replacedDbContextTypes;
        }
    }
}

