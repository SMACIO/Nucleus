using Microsoft.Extensions.Logging;

namespace Nucleus.Logging
{
    public interface IExceptionWithSelfLogging
    {
        void Log(ILogger logger);
    }
}
