using System;
using System.Runtime.Serialization;

namespace Nucleus
{
    /// <summary>
    /// Base exception type for those are thrown by Nucleus system for Nucleus specific exceptions.
    /// </summary>
    public class NucleusException : Exception
    {
        public NucleusException()
        {

        }

        public NucleusException(string message)
            : base(message)
        {

        }

        public NucleusException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public NucleusException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}



