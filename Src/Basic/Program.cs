using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(cnfg =>
    {
        cnfg.Filters.Add(new AuthorizeFilter());
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.CookiePrefix)
    .AddCookie(CookieAuthenticationDefaults.CookiePrefix, cnfg =>
    {
        cnfg.LoginPath = "/Home/Authenticate";
        cnfg.Cookie.Name = "MyCuteCookie";
    });


var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
