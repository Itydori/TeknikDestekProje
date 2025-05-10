using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TeknikServis.Entities.Auth;   //  ← AppUser burada

namespace TeknikServis.DataAccess
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider sp)
        {
            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = sp.GetRequiredService<UserManager<AppUser>>();   // tek userMgr

            // 1) Rol var mı?
            if (!await roleMgr.RoleExistsAsync("Admin"))
                await roleMgr.CreateAsync(new IdentityRole("Admin"));

            // 2) İlk admin var mı?
            const string mail = "admin@site.com";
            var admin = await userMgr.FindByEmailAsync(mail);
            if (admin == null)
            {
                admin = new AppUser { UserName = "admin", Email = mail };
                await userMgr.CreateAsync(admin, "Admin123!");
                await userMgr.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
