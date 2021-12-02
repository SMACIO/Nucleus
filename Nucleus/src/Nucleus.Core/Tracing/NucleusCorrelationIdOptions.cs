namespace Nucleus.Tracing
{
    public class NucleusCorrelationIdOptions
    {
        public string HttpHeaderName { get; set; } = "X-Correlation-Id";

        public bool SetResponseHeader { get; set; } = true;
    }
}


