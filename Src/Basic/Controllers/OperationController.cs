using Basic.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class OperationController : Controller
    {
        public async Task<IActionResult> ComeNear([FromServices] IAuthorizationService authSvc)
        {
            var comeNear = new OperationAuthorizationRequirement
            {
                Name = CookieJarOperations.ComeNear
            };
            if ((await authSvc.AuthorizeAsync(User, null, comeNear)).Succeeded)
            {
                ViewBag.SecretMessage = "You can come near my cookie jar";
            }

            return View();
        }
    }
}
