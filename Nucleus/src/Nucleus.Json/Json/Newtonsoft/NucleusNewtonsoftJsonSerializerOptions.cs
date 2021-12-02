using Newtonsoft.Json;
using Nucleus.Collections;

namespace Nucleus.Json.Newtonsoft
{
    public class NucleusNewtonsoftJsonSerializerOptions
    {
        public ITypeList<JsonConverter> Converters { get; }

        public NucleusNewtonsoftJsonSerializerOptions()
        {
            Converters = new TypeList<JsonConverter>();
        }
    }
}




