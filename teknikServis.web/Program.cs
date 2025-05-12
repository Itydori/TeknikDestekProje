using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TeknikServis.Business.Abstract;
using TeknikServis.Business.Concrete;
using TeknikServis.DataAccess;
using TeknikServis.DataAccess.Services;
using TeknikServis.Entities.Auth;
using TeknikServis.Entities.Servis;

var builder = WebApplication.CreateBuilder(args);

// ▶ DbContext
builder.Services.AddDbContext<TeknikServisDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ▶ Identity  (AppUser + IdentityRole)
builder.Services
	.AddIdentity<AppUser, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequiredLength = 6;

        opt.User.RequireUniqueEmail = true;
        opt.Lockout.AllowedForNewUsers = false;
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

    })
	.AddEntityFrameworkStores<TeknikServisDbContext>();

// ▶ DI kayıtları (senin servislerin)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IIslemRepository, IslemRepository>();
builder.Services.AddScoped<IFaturaService, FaturaService>();
builder.Services.AddScoped<IPanelReportService, PanelReportService>();
builder.Services.AddScoped<IslemIndexService>();
builder.Services.AddScoped<IRepository<IsEmriTeslim>, Repository<IsEmriTeslim>>();
builder.Services.AddScoped<IIsEmriService, IsEmriService>();
builder.Services.AddScoped<IMusteriService, MusteriService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<LoginAuditService>();

// ▶ MVC / Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ▶ Hata/kullanıcı dostu sayfalar
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();          // 1
app.UseAuthentication();   // 2
app.UseAuthorization();    // 3

// ▶ Varsayılan route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// ▶ İlk kez Elasticsearch indexleme (mevcut kodun)
var path = Path.Combine(AppContext.BaseDirectory, "indexed.flag");
if (!File.Exists(path))
{
    using var scope = app.Services.CreateScope();
    var indexSvc = scope.ServiceProvider.GetRequiredService<IslemIndexService>();
    await indexSvc.IndexAllAsync();
    File.WriteAllText(path, DateTime.Now.ToString());
}

// ▶ Migration uygula + Admin seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TeknikServisDbContext>();
    await db.Database.MigrateAsync();                 // otomatik migration
    await IdentitySeed.SeedAsync(scope.ServiceProvider); // admin/admin123 seed
}

app.Run();
