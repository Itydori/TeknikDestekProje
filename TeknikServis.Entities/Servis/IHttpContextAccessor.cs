namespace TeknikServis.DataAccess.Services
{
    internal interface IHttpContextAccessor
    {
        string? HttpContext { get; }
    }
}