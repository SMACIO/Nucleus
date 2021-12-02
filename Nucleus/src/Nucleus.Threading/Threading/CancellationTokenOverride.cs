using System.Threading;

namespace Nucleus.Threading
{
    public class CancellationTokenOverride
    {
        public CancellationToken CancellationToken { get; }

        public CancellationTokenOverride(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
        }
    }
}

