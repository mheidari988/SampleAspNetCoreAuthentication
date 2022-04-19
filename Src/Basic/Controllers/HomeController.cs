using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
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

        public async Task<IActionResult> DoStuff()
        {
            // do some stuff

            var policy = new AuthorizationPolicyBuilder("DoStuffScheme")
                .RequireClaim(ClaimTypes.UserData, "SomeData").Build();

            var result = await _authorizationService.AuthorizeAsync(User, policy);
            if (result.Succeeded)
            {
                ViewBag.SecretMessage = "You can see secret message because you have *SomeData*";
            }
            return View();
        }

        public IActionResult DoStuffInView()
        {
            return View();
        }

        public async Task<IActionResult> DoStuffInlineInject([FromServices] IAuthorizationService authorizationService)
        {
            if ((await authorizationService.AuthorizeAsync(User, "ClaimDateOfBirth")).Succeeded)
            {
                ViewBag.SecretMessage = "I'm authorized to use DoStuffInlineInject Action and here.";
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Authenticate()
        {
            var cuteClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Mohi"),
                new Claim(ClaimTypes.Email, "mohidev@outlook.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.DateOfBirth, new DateTime(1988,4,29).ToString()),
                new Claim(ClaimTypes.UserData, "SomeData")
            };

            var cuteIdentity = new ClaimsIdentity(cuteClaims, "Cute Identity");
            var cutePrincipal = new ClaimsPrincipal(cuteIdentity);

            HttpContext.SignInAsync(cutePrincipal);

            return RedirectToAction("Index");
        }
    }
}
