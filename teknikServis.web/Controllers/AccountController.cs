using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using teknikServis.web.Models.Account;
using TeknikServis.Entities.Auth;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInMgr;
    public AccountController(SignInManager<AppUser> signInMgr)
    {
        _signInMgr = signInMgr;
    }

    [HttpGet, AllowAnonymous]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var res = await _signInMgr.PasswordSignInAsync(vm.Email, vm.Password, true, false);
        if (res.Succeeded) return RedirectToAction("Index", "Panel");

        ModelState.AddModelError("", "Hatalı e-posta veya parola");
        return View(vm);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInMgr.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}
