using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Entities;
using TeknikServis.web.Models;

namespace TeknikServis.web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminsController : Controller
{
    private readonly UserManager<AppUser> _userMgr;
    private readonly RoleManager<IdentityRole> _roleMgr;

    public AdminsController(UserManager<AppUser> userMgr,
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

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(YeniAdminViewModel m)
    {
        if (!ModelState.IsValid) return View(m);

        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = m.UserName,
            Email = m.Email,
            Name = m.Name, // 👈 eklendi
            EmailConfirmed = true

        };

        var res = await _userMgr.CreateAsync(user, m.Password);
        if (res.Succeeded)
        {
            if (!await _roleMgr.RoleExistsAsync("Admin"))
                await _roleMgr.CreateAsync(new IdentityRole { Name = "Admin" });

            await _userMgr.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Index));
        }

        foreach (var e in res.Errors) ModelState.AddModelError("", e.Description);
        return View(m);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userMgr.FindByIdAsync(id);
        if (user != null) await _userMgr.DeleteAsync(user);
        return RedirectToAction(nameof(Index));
    }
}
