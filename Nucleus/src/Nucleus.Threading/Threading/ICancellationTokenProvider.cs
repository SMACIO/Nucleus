using System;
using System.Threading;

namespace Nucleus.Threading
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }

        IDisposable Use(CancellationToken cancellationToken);
    }
}

