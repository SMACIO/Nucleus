using System.Text.Json;
using Nucleus.Collections;

namespace Nucleus.Json.SystemTextJson
{
    public class NucleusSystemTextJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; }

        public ITypeList UnsupportedTypes { get; }

        public NucleusSystemTextJsonSerializerOptions()
        {
            JsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            UnsupportedTypes = new TypeList();
        }
    }
}




