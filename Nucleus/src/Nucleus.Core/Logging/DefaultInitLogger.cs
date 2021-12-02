using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Nucleus.Logging
{
    public class DefaultInitLogger<T> : IInitLogger<T>
    {
        public List<NucleusInitLogEntry> Entries { get; }

        public DefaultInitLogger()
        {
            Entries = new List<NucleusInitLogEntry>();
        }

        public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Entries.Add(new NucleusInitLogEntry
            {
                LogLevel = logLevel,
                EventId = eventId,
                State = state,
                Exception = exception,
                Formatter = (s, e) => formatter((TState)s, e),
            });
        }

        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public virtual IDisposable BeginScope<TState>(TState state)
        {
            return NullDisposable.Instance;
        }
    }
}



