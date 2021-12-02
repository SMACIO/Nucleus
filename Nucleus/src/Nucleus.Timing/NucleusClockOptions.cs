using System;

namespace Nucleus.Timing
{
    public class NucleusClockOptions
    {
        /// <summary>
        /// Default: <see cref="DateTimeKind.Unspecified"/>
        /// </summary>
        public DateTimeKind Kind { get; set; }

        public NucleusClockOptions()
        {
            Kind = DateTimeKind.Unspecified;
        }
    }
}


