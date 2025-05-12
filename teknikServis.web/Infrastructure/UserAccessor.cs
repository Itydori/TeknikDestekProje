using System.Security.Claims;

public interface IUserAccessor { string? GetUserId(); }

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _ctx;
    public UserAccessor(IHttpContextAccessor ctx) => _ctx = ctx;
    public string? GetUserId() =>
        _ctx.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
