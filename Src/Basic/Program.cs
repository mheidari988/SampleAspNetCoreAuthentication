using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

ConfigLogging(builder);
ConfigControllersWithViews(builder);
ConfigAuthentication(builder);
ConfigAuthorization(builder);

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();

static void ConfigureScoped(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IAuthorizationHandler, CustomPolicyHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();
}
static void ConfigLogging(WebApplicationBuilder builder)
{
    builder.Services.AddLogging(cnfg =>
    {
        cnfg.AddConsole()
            .AddDebug();
    });
}
static void ConfigControllersWithViews(WebApplicationBuilder builder)
{
    builder.Services.AddControllersWithViews(cnfg =>
    {
        cnfg.Filters.Add(new AuthorizeFilter());
    })
        .AddRazorRuntimeCompilation();
}
static void ConfigAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.CookiePrefix)
        .AddCookie(CookieAuthenticationDefaults.CookiePrefix, cnfg =>
        {
            cnfg.LoginPath = "/Home/Authenticate";
            cnfg.Cookie.Name = "MyCuteCookie";
        });
}
static void ConfigAuthorization(WebApplicationBuilder builder)
{
    builder.Services.AddAuthorization(cnfg =>
    {
        cnfg.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            //.RequireClaim(ClaimTypes.DateOfBirth)
            .Build();
    });
}