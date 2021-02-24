using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkierQuartz.Models;
using System;
using System.Threading.Tasks;

namespace SilkierQuartz.Controllers
{
    [AllowAnonymous]
    public class AuthenticateController : PageControllerBase
    {
        private readonly SilkierQuartzAuthenticateOptions _authenticateOptions;

        public AuthenticateController(SilkierQuartzAuthenticateOptions authenticateOptions)
        {
            _authenticateOptions = authenticateOptions;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(_authenticateOptions.UserName) ? "SilkierQuartzAdmin" : _authenticateOptions.UserName ),
            //    new Claim(ClaimTypes.Name, string.IsNullOrEmpty(_authenticateOptions.Password) ? "SilkierQuartzPassword" : _authenticateOptions.Password),
            //    new Claim(SilkierQuartzAuthenticateOptions.SilkierQuartzSpecificClaim, "Authorized")
            //};

            //var authProperties = new AuthenticationProperties()
            //{
            //    IsPersistent = _authenticateOptions.IsPersist ?? false
            //};

            //var userIdentity = new ClaimsIdentity(claims, "login");
            //await HttpContext.SignInAsync(SilkierQuartzAuthenticateConfig.AuthScheme, new ClaimsPrincipal(userIdentity),
            //    authProperties);

            return View(new AuthenticateViewModel()
            {
                UserName = "this is test",
                Password = "this is password"
            });
        }

        [HttpPost]
        public IActionResult Login(AuthenticateViewModel request)
        {
            Console.WriteLine(request.UserName);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(SilkierQuartzAuthenticateConfig.AuthScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
