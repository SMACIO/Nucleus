using System.Collections.Generic;

namespace Nucleus.AspNetCore.Uow
{
    public class AspNetCoreUnitOfWorkTransactionBehaviourProviderOptions
    {
        public List<string> NonTransactionalUrls { get; }

        public AspNetCoreUnitOfWorkTransactionBehaviourProviderOptions()
        {
            NonTransactionalUrls = new List<string>
            {
                "/connect/"
            };
        }
    }
}

