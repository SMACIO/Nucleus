using System;
using System.Collections.Generic;

namespace Nucleus.Data
{
    [Serializable]
    public class ExtraPropertyDictionary : Dictionary<string, object>
    {
        public ExtraPropertyDictionary()
        {

        }

        public ExtraPropertyDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }
    }
}

