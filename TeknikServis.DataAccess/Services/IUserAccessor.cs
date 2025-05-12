namespace TeknikServis.DataAccess.Services
{
    public interface IUserAccessor
    {
        string? GetCurrentUserId();
        string? GetCurrentIpAddress();
    }
}