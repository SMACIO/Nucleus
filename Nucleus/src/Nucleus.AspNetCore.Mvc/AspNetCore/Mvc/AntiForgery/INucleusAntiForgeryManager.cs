namespace Nucleus.AspNetCore.Mvc.AntiForgery
{
    public interface INucleusAntiForgeryManager
    {
        void SetCookie();

        string GenerateToken();
    }
}


