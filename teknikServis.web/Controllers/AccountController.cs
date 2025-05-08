using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeknikServis.Entities.Auth;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInMgr;
    public AccountController(SignInManager<AppUser> m) => _signInMgr = m;

    [HttpGet] public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var result = await _signInMgr.PasswordSignInAsync(email, password, true, false);
        if (result.Succeeded) return RedirectToAction("Index", "Panel");   // panel ana sayfan
        ModelState.AddModelError("", "Hatalı giriş");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInMgr.SignOutAsync();
        return RedirectToAction("Login");
    }
}
