using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<HomeController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is not null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation($"Username: {username} has been logged in.");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Username: {username} has been registered.");
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    _logger.LogInformation($"Username: {username} has been registered.");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"Username: {User?.Identity?.Name} has been logged out");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
