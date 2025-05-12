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

    [HttpGet]
    public async Task<IActionResult> UpdatePassword(string id)
    {
        var user = await _userMgr.FindByIdAsync(id);
        if (user == null) return NotFound();

        var vm = new UpdatePasswordViewModel
        {
            Id = user.Id,
            Email = user.Email!
        };
        return View(vm);              // Views/Admins/UpdatePassword.cshtml
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel m)
    {
        if (!ModelState.IsValid) return View(m);

        var user = await _userMgr.FindByIdAsync(m.Id);
        if (user == null) return NotFound();

        // Önce reset token üret → sonra ResetPasswordAsync
        var token = await _userMgr.GeneratePasswordResetTokenAsync(user);
        var res = await _userMgr.ResetPasswordAsync(user, token, m.NewPassword);

        if (res.Succeeded)
        {
            TempData["ok"] = "Şifre güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        foreach (var e in res.Errors)
            ModelState.AddModelError("", e.Description);

        return View(m);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var me = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (id == me)
        {
            TempData["err"] = "Kendi hesabını silemezsin.";
            return RedirectToAction(nameof(Index));
        }

        var user = await _userMgr.FindByIdAsync(id);
        if (user != null) await _userMgr.DeleteAsync(user);

        TempData["ok"] = "Admin silindi.";
        return RedirectToAction(nameof(Index));
    }
}
