using System;
using Nucleus.ExceptionHandling;

namespace Nucleus.GlobalFeatures
{
    [Serializable]
    public class NucleusGlobalFeatureNotEnabledException : NucleusException, IHasErrorCode
    {
        public string Code { get; }

        public NucleusGlobalFeatureNotEnabledException(string message = null, string code = null, Exception innerException = null)
            : base(message, innerException)
        {
            Code = code;
        }

        public NucleusGlobalFeatureNotEnabledException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}





