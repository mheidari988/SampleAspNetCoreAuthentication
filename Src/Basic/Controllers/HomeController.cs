using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Secret()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Authenticate()
        {
            var cuteClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Mohi"),
                new Claim(ClaimTypes.Email,"mohidev@outlook.com")
            };

            var cuteIdentity = new ClaimsIdentity(cuteClaims, "Cute Identity");
            var cutePrincipal = new ClaimsPrincipal(cuteIdentity);

            HttpContext.SignInAsync(cutePrincipal);

            return RedirectToAction("Index");
        }
    }
}
