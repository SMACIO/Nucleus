using System;

namespace Nucleus.Data
{
    public class NucleusDbConcurrencyException : NucleusException
    {
        /// <summary>
        /// Creates a new <see cref="NucleusDbConcurrencyException"/> object.
        /// </summary>
        public NucleusDbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NucleusDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public NucleusDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="NucleusDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NucleusDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}




