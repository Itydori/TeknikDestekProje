using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TeknikServis.Business.Abstract;
using TeknikServis.Business.Concrete;
using TeknikServis.DataAccess;
using TeknikServis.Entities.Servis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TeknikServisDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Services.AddIdentity<Kullanici, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<TeknikServisDbContext>()
    .AddDefaultTokenProviders();
    
 // AddUserManager eklenmeli


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IIslemRepository,IslemRepository>();
builder.Services.AddScoped<IFaturaService, FaturaService>();

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
        pattern: "{controller=Home}/{action=Index}/{id:int?}");
});
app.MapRazorPages();

app.Run();

