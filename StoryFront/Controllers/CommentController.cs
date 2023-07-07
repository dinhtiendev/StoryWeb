using Microsoft.AspNetCore.Mvc;

namespace StoryFront.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
