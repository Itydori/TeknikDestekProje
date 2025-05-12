using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using teknikServis.Entities;
using teknikServis.web.Models.Account;
using TeknikServis.Entities;
using TeknikServis.Entities.Auth;          // AppUser burada

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(SignInManager<AppUser> signInManager,
                             UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel m, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(m);

        // 1. Kullanıcı adını e-posta da olabilir
        var user = await _userManager.FindByNameAsync(m.UserName)
                   ?? await _userManager.FindByEmailAsync(m.UserName);

        if (user == null)
        {
            ModelState.AddModelError("", "Kullanıcı bulunamadı");
            return View(m);
        }

        // 2. Parola doğrula
        var res = await _signInManager.PasswordSignInAsync(user, m.Password,
                                                       m.RememberMe, lockoutOnFailure: false);
        if (!res.Succeeded)
        {
            ModelState.AddModelError("", "Şifre hatalı");
            return View(m);
        }

        // 3. YÖNLENDİRME ↓
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        // Admin ise direk admin paneline; değilse Panel ana sayfası
        return await _userManager.IsInRoleAsync(user, "Admin")
               ? RedirectToAction("Index", "Admins")
               : RedirectToAction("Index", "Panel");
    }


    // Çıkış
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();          // ⇦ cookie temizlenir
        return RedirectToAction("Index", "Home");     // ⇦ nereye istersen
    }
}
