using System;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Nucleus.ExceptionHandling;
using Nucleus.Logging;

namespace Nucleus.Authorization
{
    /// <summary>
    /// This exception is thrown on an unauthorized request.
    /// </summary>
    [Serializable]
    public class NucleusAuthorizationException : NucleusException, IHasLogLevel, IHasErrorCode
    {
        /// <summary>
        /// Severity of the exception.
        /// Default: Warn.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Creates a new <see cref="NucleusAuthorizationException"/> object.
        /// </summary>
        public NucleusAuthorizationException()
        {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// Creates a new <see cref="NucleusAuthorizationException"/> object.
        /// </summary>
        public NucleusAuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="NucleusAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public NucleusAuthorizationException(string message)
            : base(message)
        {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// Creates a new <see cref="NucleusAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NucleusAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// Creates a new <see cref="NucleusAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="code">Exception code</param>
        /// <param name="innerException">Inner exception</param>
        public NucleusAuthorizationException(string message = null, string code = null, Exception innerException = null)
            : base(message, innerException)
        {
            Code = code;
            LogLevel = LogLevel.Warning;
        }

        public NucleusAuthorizationException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}






