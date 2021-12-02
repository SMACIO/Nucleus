using Nucleus.Collections;
using Nucleus.Json.SystemTextJson;

namespace Nucleus.Json
{
    public class NucleusJsonOptions
    {
        /// <summary>
        /// Used to set default value for the DateTimeFormat.
        /// </summary>
        public string DefaultDateTimeFormat { get; set; }

        /// <summary>
        /// It will try to use System.Json.Text to handle JSON if it can otherwise use Newtonsoft.
        /// Affects both NucleusJsonModule and NucleusAspNetCoreMvcModule.
        /// See <see cref="NucleusSystemTextJsonUnsupportedTypeMatcher"/>
        /// </summary>
        public bool UseHybridSerializer { get; set; }

        public ITypeList<IJsonSerializerProvider> Providers { get; }

        public NucleusJsonOptions()
        {
            Providers = new TypeList<IJsonSerializerProvider>();
            UseHybridSerializer = true;
        }
    }
}





