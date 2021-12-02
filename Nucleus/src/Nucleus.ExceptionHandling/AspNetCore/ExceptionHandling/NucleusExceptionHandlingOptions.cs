namespace Nucleus.AspNetCore.ExceptionHandling
{
    public class NucleusExceptionHandlingOptions
    {
        public bool SendExceptionsDetailsToClients { get; set; } = false;

        public bool SendStackTraceToClients { get; set; } = true;
    }
}


