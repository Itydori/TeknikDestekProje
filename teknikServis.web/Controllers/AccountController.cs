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

    // POST: /Account/Login
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model,
                                           string returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);   // KALSIN
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
                         user, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // Güvenli local redirect
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Panel");   // yedek
        }

        ModelState.AddModelError(string.Empty, "Hatalı giriş denemesi");
        return View(model);
    }

    // Çıkış
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();          // ⇦ cookie temizlenir
        return RedirectToAction("Index", "Home");     // ⇦ nereye istersen
    }
}
