using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkierQuartz.Models;

namespace SilkierQuartz.Controllers
{
    [AllowAnonymous]
    public class AuthenticateController : PageControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
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
    }
}
