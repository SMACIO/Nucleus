using System.Threading.Tasks;
using Nucleus.DependencyInjection;

namespace Nucleus.Features
{
    public abstract class FeatureValueProvider : IFeatureValueProvider, ITransientDependency
    {
        public abstract string Name { get; }

        protected IFeatureStore FeatureStore { get; }

        protected FeatureValueProvider(IFeatureStore featureStore)
        {
            FeatureStore = featureStore;
        }

        public abstract Task<string> GetOrNullAsync(FeatureDefinition feature);
    }
}

