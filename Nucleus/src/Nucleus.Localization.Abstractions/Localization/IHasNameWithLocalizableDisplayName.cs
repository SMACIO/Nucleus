using JetBrains.Annotations;

namespace Nucleus.Localization
{
    public interface IHasNameWithLocalizableDisplayName
    {
        [NotNull]
        public string Name { get; }

        [CanBeNull]
        public ILocalizableString DisplayName { get; }
    }
}
