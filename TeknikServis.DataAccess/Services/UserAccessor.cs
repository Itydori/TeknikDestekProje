using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TeknikServis.DataAccess.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public string? GetCurrentUserId()
            => _httpContextAccessor.HttpContext
               ?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? GetCurrentIpAddress()
            => _httpContextAccessor.HttpContext
               ?.Connection.RemoteIpAddress?.ToString();
    }
}
