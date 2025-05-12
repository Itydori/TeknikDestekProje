public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserAccessor _user;
    public AuditSaveChangesInterceptor(IUserAccessor user) => _user = user;
    // … (SavingChanges / SavedChanges event’lerinde AuditLog üret)
}
