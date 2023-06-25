using System;
using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{
	public class AuthController : Controller
	{
		public AuthController()
		{
		}

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}

