using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using teknikServis.Entities.Infrastructure;   // ← Arayüzü görüyoruz

namespace TeknikServis.DataAccess.Interceptors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _ctx;

        public UserAccessor(IHttpContextAccessor ctx) => _ctx = ctx;

        public string? GetCurrentUserId() =>
            _ctx.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? GetCurrentIpAddress() =>
            _ctx.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }
}
