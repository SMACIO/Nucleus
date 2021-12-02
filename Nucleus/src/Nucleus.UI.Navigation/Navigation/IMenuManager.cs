using System.Threading.Tasks;

namespace Nucleus.UI.Navigation
{
    public interface IMenuManager
    {
        Task<ApplicationMenu> GetAsync(string name);
        
        Task<ApplicationMenu> GetMainMenuAsync();
    }
}

