using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(cnfg =>
{
    cnfg.UseInMemoryDatabase("InMemDb");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(cnfg =>
{
    cnfg.Password.RequiredLength = 4;
    cnfg.Password.RequireNonAlphanumeric = false;
    cnfg.Password.RequireDigit = false;
    cnfg.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(cnfg =>
{
    cnfg.LoginPath = "/Home/Login";
    cnfg.LogoutPath = "/Home/Logout";
});
builder.Services.AddControllersWithViews(cnfg =>
{
    cnfg.Filters.Add(new AuthorizeFilter());
}).AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
