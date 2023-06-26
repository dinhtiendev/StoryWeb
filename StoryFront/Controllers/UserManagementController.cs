using System;
using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{
	public class UserManagementController : Controller
	{
		public UserManagementController()
		{
		}

        public IActionResult UserIndex()
        {
            return View();
        }

        public IActionResult UserCreate()
        {
            return View();
        }

        public IActionResult UserEdit()
        {
            return View();
        }

        public IActionResult UserDelete()
        {
            return View();
        }
    }
}

