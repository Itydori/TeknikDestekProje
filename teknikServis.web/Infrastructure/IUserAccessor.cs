namespace TeknikServis.DataAccess.Interceptors
{
    public interface IUserAccessor
    {
        string? GetCurrentUserId();
        string? GetCurrentIpAddress();
    }
}
