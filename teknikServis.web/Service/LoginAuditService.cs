using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TeknikServis.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TeknikServis.DataAccess.Services   // kendi namespace’ine göre ayarla
{
    public class LoginAuditService
    {
        private readonly TeknikServisDbContext _db;          // ← alanın adı _db (veya _ctx)
        private readonly IHttpContextAccessor _http;

        public LoginAuditService(TeknikServisDbContext db, IHttpContextAccessor http)
        {
            _db = db;      // ctor’da atıyoruz
            _http = http;
        }

        public async Task AddAsync(AppUser? user, bool success)
        {
            var ip = _http.HttpContext?.Connection.RemoteIpAddress?.ToString();

            var audit = new LoginAudit
            {
                UserId = user?.Id,
                UserName = user?.UserName ?? "(unknown)",
                TimeUtc = DateTime.UtcNow,
                Success = success,
                IpAddress = ip
            };

            _db.LoginAudits.Add(audit);               // artık _db tanımlı
            await _db.SaveChangesAsync();
        }
    }
}
