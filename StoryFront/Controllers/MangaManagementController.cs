using System;
using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{ 
	public class MangaManagementController : Controller
	{
		public MangaManagementController()
		{
		}

        public IActionResult ListManga()
        {
            return View();
        }
    }
}

