using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Entities.Auth;
using TeknikServis.Entities.Servis;

[Authorize(Roles = "Admin")]
public class AdminsController : Controller
{
    private readonly UserManager<AppUser> _userMgr;
    private readonly RoleManager<IdentityRole> _roleMgr;

    public AdminsController(
        UserManager<AppUser> userMgr,
        RoleManager<IdentityRole> roleMgr)
    {
        _userMgr = userMgr;
        _roleMgr = roleMgr;
    }

    public async Task<IActionResult> Index()
    {
        var admins = await _userMgr.GetUsersInRoleAsync("Admin");
        return View(admins);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string email, string password)
    {
        var user = new AppUser { UserName = email, Email = email };
        var res = await _userMgr.CreateAsync(user, password);

        if (res.Succeeded)
            await _userMgr.AddToRoleAsync(user, "Admin");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userMgr.FindByIdAsync(id);
        if (user != null) await _userMgr.DeleteAsync(user);
        return RedirectToAction(nameof(Index));
    }
}
