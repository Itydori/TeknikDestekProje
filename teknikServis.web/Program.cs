using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TeknikServis.Business.Abstract;
using TeknikServis.Business.Concrete;
using TeknikServis.DataAccess;
using TeknikServis.Entities.Servis;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TeknikServisDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IPanelReportService, PanelReportService>();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Services.AddIdentity<Kullanici, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<TeknikServisDbContext>()
    .AddDefaultTokenProviders();

// AddUserManager eklenmeli




//builder.Services.AddModelStateExtension();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IIslemRepository,IslemRepository>();
builder.Services.AddScoped<IFaturaService, FaturaService>();

builder.Services.AddScoped<IPanelReportService, PanelReportService>();
builder.Services.AddScoped<IslemIndexService>();


builder.Services.AddScoped<IIsEmriService, IsEmriService>();
builder.Services.AddScoped<IMusteriService, MusteriService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseAuthorization();


app.UseRouting();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.MapRazorPages();

var path = Path.Combine(AppContext.BaseDirectory, "indexed.flag");

if (!File.Exists("indexed.flag"))
{
    using (var scope = app.Services.CreateScope())
    {
        var indexService = scope.ServiceProvider.GetRequiredService<IslemIndexService>();
        await indexService.IndexAllAsync(); // indexlenmesi için
        File.WriteAllText("indexed.flag", DateTime.Now.ToString());
    }
}
app.Run();

