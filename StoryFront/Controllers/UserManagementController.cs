using System;
using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{
	public class UserManagementController : Controller
	{
		public UserManagementController()
		{
		}

        public IActionResult ListUser()
        {
            return View();
        }
    }
}

