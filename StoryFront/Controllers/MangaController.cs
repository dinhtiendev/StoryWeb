using System;
using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{
	public class MangaController : Controller
    {

        public MangaController() {
        }

        public IActionResult MangaDetail()
        {
            return View();
        }

        public IActionResult SearchManga()
        {
            return View();
        }

        public IActionResult ReadManga()
        {
            return View();
        }
    }
}

