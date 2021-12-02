using System.Threading.Tasks;
using Nucleus.AspNetCore.Mvc.ApplicationConfigurations;
using Nucleus.Caching;
using Nucleus.DependencyInjection;
using Nucleus.EventBus;
using Nucleus.Users;

namespace Nucleus.AspNetCore.Mvc.Client
{
    public class MvcCurrentApplicationConfigurationCacheResetEventHandler :
        ILocalEventHandler<CurrentApplicationConfigurationCacheResetEventData>,
        ITransientDependency
    {
        protected ICurrentUser CurrentUser { get; }
        protected IDistributedCache<ApplicationConfigurationDto> Cache { get; }

        public MvcCurrentApplicationConfigurationCacheResetEventHandler(ICurrentUser currentUser,
            IDistributedCache<ApplicationConfigurationDto> cache)
        {
            CurrentUser = currentUser;
            Cache = cache;
        }

        public virtual async Task HandleEventAsync(CurrentApplicationConfigurationCacheResetEventData eventData)
        {
            await Cache.RemoveAsync(CreateCacheKey());
        }

        protected virtual string CreateCacheKey()
        {
            return MvcCachedApplicationConfigurationClientHelper.CreateCacheKey(CurrentUser);
        }
    }
}


