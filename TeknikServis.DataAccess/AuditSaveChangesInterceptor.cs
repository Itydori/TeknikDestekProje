using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using teknikServis.Entities;
using TeknikServis.DataAccess.Services;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserAccessor _userAccessor;
    public AuditSaveChangesInterceptor(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            CreateAuditLogs(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            CreateAuditLogs(eventData.Context);
        }

        return base.SavingChanges(eventData, result);
    }

    private void CreateAuditLogs(DbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added
                     || e.State == EntityState.Modified
                     || e.State == EntityState.Deleted)
            .ToList();

        if (!entries.Any())
            return;

        string adminId = _userAccessor.GetCurrentUserId();
        string ipAddress = _userAccessor.GetCurrentIpAddress();
        DateTime now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            // Skip AuditLog entities to avoid infinite loop
            if (entry.Entity is AuditLog)
                continue;

            var auditLog = new AuditLog
            {
                AdminId = adminId,
                Action = GetAction(entry.State),
                Entity = entry.Metadata.ClrType.Name,
                EntityKey = GetPrimaryKeyValue(entry),
                OldValues = entry.State == EntityState.Added ? null : GetOldValues(entry),
                NewValues = entry.State == EntityState.Deleted ? null : GetNewValues(entry),
                When = now,
                IpAddress = ipAddress
            };

            context.Set<AuditLog>().Add(auditLog);
        }
    }

    private string GetAction(EntityState state)
    {
        return state switch
        {
            EntityState.Added => "Created",
            EntityState.Modified => "Updated",
            EntityState.Deleted => "Deleted",
            _ => string.Empty
        };
    }

    private string GetPrimaryKeyValue(EntityEntry entry)
    {
        var keyValues = entry.Metadata.FindPrimaryKey()
            .Properties
            .Select(p => entry.Property(p.Name).CurrentValue?.ToString())
            .ToArray();

        return string.Join(",", keyValues);
    }

    private string GetOldValues(EntityEntry entry)
    {
        var values = new Dictionary<string, object>();

        foreach (var property in entry.Properties)
        {
            // Skip primary keys, navigation properties
            if (property.Metadata.IsPrimaryKey() || property.Metadata.IsKey())
                continue;

            if (entry.State == EntityState.Modified && property.IsModified)
            {
                values[property.Metadata.Name] = property.OriginalValue;
            }
            else if (entry.State == EntityState.Deleted)
            {
                values[property.Metadata.Name] = property.OriginalValue;
            }
        }

        return JsonSerializer.Serialize(values);
    }

    private string GetNewValues(EntityEntry entry)
    {
        var values = new Dictionary<string, object>();

        foreach (var property in entry.Properties)
        {
            // Skip primary keys, navigation properties
            if (property.Metadata.IsPrimaryKey() || property.Metadata.IsKey())
                continue;

            if (entry.State == EntityState.Added ||
               (entry.State == EntityState.Modified && property.IsModified))
            {
                values[property.Metadata.Name] = property.CurrentValue;
            }
        }

        return JsonSerializer.Serialize(values);
    }
}