using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{
	public class MangaController : Controller
    {
        private readonly IStoryService _storyService;
        private readonly ICommentService _commentService;

        public MangaController(IStoryService storyService, ICommentService commentService)
        {
            _storyService = storyService;
            _commentService = commentService;
        }

        public async Task<IActionResult> MangaDetail(int storyId)
        {
            storyId = 1;
            var responseC = await _commentService.GetAllAsync<ResponseDto>(storyId, null);
            var responseM = await _storyService.GetStoryByIdAsync<ResponseDto>(storyId, null);
            if (responseM.IsSuccess && responseC.IsSuccess)
            {
                var comments = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(Convert.ToString(responseC.Result));
                var manga = JsonConvert.DeserializeObject<StoryDTO>(Convert.ToString(responseM.Result));
                ViewBag.Manga = manga;
                ViewBag.Comments = comments;
                return View("Views/Manga/MangaDetail.cshtml");
            }
            return NotFound();
        }

        public async Task<IActionResult> SearchManga()
        {
            return View();
        }

        public async Task<IActionResult> ReadManga()
        {
            return View();
        }
    }
}

