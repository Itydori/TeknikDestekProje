using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeknikServis.DataAccess;
using teknikServis.web.Models;

namespace teknikServis.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuditLogController : Controller
    {
        private readonly TeknikServisDbContext _context;

        public AuditLogController(TeknikServisDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            DateTime? fromDate,
            DateTime? toDate,
            string entity,
            string adminId,
            int page = 1,
            int pageSize = 20)
        {
            // Default date range to last 7 days if not specified
            if (!fromDate.HasValue)
                fromDate = DateTime.UtcNow.AddDays(-7);

            if (!toDate.HasValue)
                toDate = DateTime.UtcNow;

            // Query for audit logs with filtering
            var query = _context.AuditLogs
                .Include(a => a.Admin)
                .Where(a => a.When >= fromDate && a.When <= toDate);

            if (!string.IsNullOrEmpty(entity))
                query = query.Where(a => a.Entity == entity);

            if (!string.IsNullOrEmpty(adminId))
                query = query.Where(a => a.AdminId == adminId);

            // Get distinct entity types and admins for filter dropdowns
            var entities = await _context.AuditLogs
                .Select(a => a.Entity)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync();

            var admins = await _context.Users
                .OrderBy(u => u.UserName)
                .Select(u => new { Id = u.Id, Name = u.UserName })
                .ToListAsync();

            // Calculate pagination
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var auditLogs = await query
                .OrderByDescending(a => a.When)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Build view model
            var viewModel = new AuditLogViewModel
            {
                AuditLogs = auditLogs,
                FromDate = fromDate.Value,
                ToDate = toDate.Value,
                SelectedEntity = entity,
                SelectedAdminId = adminId,
                EntityList = new SelectList(entities),
                AdminList = new SelectList(admins, "Id", "Name"),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var auditLog = await _context.AuditLogs
                .Include(a => a.Admin)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auditLog == null)
                return NotFound();

            return View(auditLog);
        }
    }
}