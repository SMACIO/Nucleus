using System.Collections.Generic;
using JetBrains.Annotations;

namespace Nucleus.UI.Navigation
{
    public class NucleusNavigationOptions
    {
        [NotNull]
        public List<IMenuContributor> MenuContributors { get; }
        
        /// <summary>
        /// Includes the <see cref="StandardMenus.Main"/> by default.
        /// </summary>
        public List<string> MainMenuNames { get; }

        public NucleusNavigationOptions()
        {
            MenuContributors = new List<IMenuContributor>();
            
            MainMenuNames = new List<string>
            {
                StandardMenus.Main
            };
        }
    }
}


