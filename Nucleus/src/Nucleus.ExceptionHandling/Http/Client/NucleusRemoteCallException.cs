using System;
using System.Runtime.Serialization;
using Nucleus.ExceptionHandling;

namespace Nucleus.Http.Client
{
    [Serializable]
    public class NucleusRemoteCallException : NucleusException, IHasErrorCode, IHasErrorDetails, IHasHttpStatusCode
    {
        public int HttpStatusCode { get; set; }

        public string Code => Error?.Code;

        public string Details => Error?.Details;

        public RemoteServiceErrorInfo Error { get; set; }

        public NucleusRemoteCallException()
        {

        }

        public NucleusRemoteCallException(string message, Exception innerException = null)
            : base(message, innerException)
        {

        }

        public NucleusRemoteCallException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public NucleusRemoteCallException(RemoteServiceErrorInfo error, Exception innerException = null)
            : base(error.Message, innerException)
        {
            Error = error;

            if (error.Data != null)
            {
                foreach (var dataKey in error.Data.Keys)
                {
                    Data[dataKey] = error.Data[dataKey];
                }
            }
        }
    }
}





