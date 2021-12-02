namespace Nucleus.Auditing
{
    public interface IAuditSerializer
    {
        string Serialize(object obj);
    }
}
