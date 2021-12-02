using System;
using System.Collections.Generic;

namespace Nucleus.Data
{
    public class NucleusDataFilterOptions
    {
        public Dictionary<Type, DataFilterState> DefaultStates { get; }

        public NucleusDataFilterOptions()
        {
            DefaultStates = new Dictionary<Type, DataFilterState>();
        }
    }
}



