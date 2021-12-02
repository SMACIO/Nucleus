using System;
using System.Runtime.Serialization;

namespace Nucleus
{
    public class NucleusShutdownException : NucleusException
    {
        public NucleusShutdownException()
        {

        }

        public NucleusShutdownException(string message)
            : base(message)
        {

        }

        public NucleusShutdownException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public NucleusShutdownException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}


