using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Nucleus.Logging
{
    public interface IInitLogger<out T> : ILogger<T>
    {
        public List<NucleusInitLogEntry> Entries { get; }
    }
}


