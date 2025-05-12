namespace TeknikServis.Entities.Servis
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string AdminId { get; set; }          // FK → AspNetUsers.Id
        public string Action { get; set; }           // "Create","Update","Delete"…
        public string Entity { get; set; }           // "IsEmri","Parca","Admin"…
        public string EntityKey { get; set; }        // "42" veya GUID
        public string? OldValues { get; set; }       // JSON (null ise ekleme)
        public string? NewValues { get; set; }       // JSON (null ise silme)
        public DateTime When { get; set; }
        public string? IpAddress { get; set; }       // istersen
    }
}