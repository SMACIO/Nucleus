using System.Linq;
using Nucleus.Domain.Entities;

namespace Nucleus.Auditing
{
    public static class EntityHistorySelectorListExtensions
    {
        public const string AllEntitiesSelectorName = "Nucleus.Entities.All";

        public static void AddAllEntities(this IEntityHistorySelectorList selectors)
        {
            if (selectors.Any(s => s.Name == AllEntitiesSelectorName))
            {
                return;
            }

            selectors.Add(new NamedTypeSelector(AllEntitiesSelectorName, t => typeof(IEntity).IsAssignableFrom(t)));
        }
    }
}



