using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using teknikServis.Entities;

namespace teknikServis.web.Models
{
    public class AuditLogViewModel
    {
        public List<AuditLog> AuditLogs { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SelectedEntity { get; set; }
        public string SelectedAdminId { get; set; }
        public SelectList EntityList { get; set; }
        public SelectList AdminList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}