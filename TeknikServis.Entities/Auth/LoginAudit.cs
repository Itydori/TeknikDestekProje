using System;

namespace TeknikServis.Entities.Auth
{
    public class LoginAudit
    {
        public int Id { get; set; }
        public string UserId { get; set; }     // AspNetUsers FK
        public string UserName { get; set; }
        public DateTime TimeUtc { get; set; }
        public bool Success { get; set; }
        public string? IpAddress { get; set; }
    }
}
