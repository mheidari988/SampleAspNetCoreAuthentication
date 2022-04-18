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

        [Authorize(Policy = "ClaimDateOfBirth")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminSecret()
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public IActionResult Authenticate()
        {
            var cuteClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Mohi"),
                new Claim(ClaimTypes.Email, "mohidev@outlook.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.DateOfBirth, new DateTime(1988,4,29).ToString())
            };

            var cuteIdentity = new ClaimsIdentity(cuteClaims, "Cute Identity");
            var cutePrincipal = new ClaimsPrincipal(cuteIdentity);

            HttpContext.SignInAsync(cutePrincipal);

            return RedirectToAction("Index");
        }
    }
}
