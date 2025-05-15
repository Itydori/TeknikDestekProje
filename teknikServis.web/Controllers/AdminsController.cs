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
        // 1) Önce input doğrulaması
        if (!ModelState.IsValid) return View(m);

        /* 2) Kullanıcı adı veya e-posta daha önce alınmış mı? */
        var existingByName = await _userMgr.FindByNameAsync(m.UserName);
        if (existingByName is not null)
        {
            ModelState.AddModelError(nameof(m.UserName), "Bu kullanıcı adı zaten kullanımda.");
            return View(m);
        }

        var existingByEmail = await _userMgr.FindByEmailAsync(m.Email);
        if (existingByEmail is not null)
        {
            ModelState.AddModelError(nameof(m.Email), "Bu e-posta adresi zaten kayıtlı.");
            return View(m);
        }

        /* 3) Yeni Admin kullanıcısını oluştur */
        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = m.UserName,
            Email = m.Email,
            Name = m.Name,
            EmailConfirmed = true
        };

        var res = await _userMgr.CreateAsync(user, m.Password);
        if (res.Succeeded)
        {
            // Rol yoksa önce oluştur
            if (!await _roleMgr.RoleExistsAsync("Admin"))
                await _roleMgr.CreateAsync(new IdentityRole { Name = "Admin" });

            await _userMgr.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Index));
        }

        /* 4) Diğer Identity hataları */
        foreach (var e in res.Errors)
            ModelState.AddModelError("", e.Description);

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

        // Mevcut şifreyi getiriyoruz (güvenli değil ama kıyas için gerekli değil)
        var passwordHasher = new PasswordHasher<AppUser>();
        var verification = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, m.NewPassword);

        if (verification == PasswordVerificationResult.Success)
        {
            ModelState.AddModelError(nameof(m.NewPassword), "Yeni şifre mevcut şifreyle aynı olamaz.");
            return View(m);
        }

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
