using System;
using System.Runtime.Serialization;

namespace Nucleus
{
    public class NucleusInitializationException : NucleusException
    {
        public NucleusInitializationException()
        {

        }

        public NucleusInitializationException(string message)
            : base(message)
        {

        }

        public NucleusInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public NucleusInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}


