using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{
    public class CommentController : Controller
    {

        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> ListCommentsAsync()
        {
            var response = await _commentService.GetAllAsync<ResponseDto>(1, null);
            if (response.IsSuccess)
            {
                var listC = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(Convert.ToString(response.Result));
                ViewBag.ListC = listC;
                return View("Views/Manga/MangaTestComment.cshtml");
            }
            return NotFound();
        }
    }
}
