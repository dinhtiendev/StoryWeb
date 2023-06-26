using System;
using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{ 
	public class MangaManagementController : Controller
	{
		public MangaManagementController()
		{
		}

        public IActionResult MangaIndex()
        {
            return View();
        }

        public IActionResult MangaCreate()
        {
            return View();
        }

        public IActionResult MangaEdit()
        {
            return View();
        }

        public IActionResult MangaDelete()
        {
            return View();
        }
    }
}

