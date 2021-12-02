using System.Threading.Tasks;

namespace Nucleus.AspNetCore.Mvc.Localization
{
    public interface IQueryStringCultureReplacement
    {
        Task ReplaceAsync(QueryStringCultureReplacementContext context);
    }
}

