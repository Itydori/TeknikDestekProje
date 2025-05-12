using System;
using System.ComponentModel.DataAnnotations;
using TeknikServis.Entities;

namespace teknikServis.Entities
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }
        public string AdminId { get; set; }
        public string Action { get; set; }  // Created, Updated, Deleted
        public string Entity { get; set; }  // The entity type name
        public string EntityKey { get; set; }  // The primary key value
        public string OldValues { get; set; }  // JSON representation of old values
        public string NewValues { get; set; }  // JSON representation of new values
        public DateTime When { get; set; }
        public string IpAddress { get; set; }

        // Navigation property
        public AppUser Admin { get; set; }
    }
}