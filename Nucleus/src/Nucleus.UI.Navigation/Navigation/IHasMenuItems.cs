using Nucleus.UI.Navigation;

namespace Nucleus.UI.Navigation
{
    public interface IHasMenuItems
    {
        /// <summary>
        /// Menu items.
        /// </summary>
        ApplicationMenuItemList Items { get; }
    }
}

