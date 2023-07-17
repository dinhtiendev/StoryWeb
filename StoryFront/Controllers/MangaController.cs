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
        private readonly ICategoryService _categoryService;
        private readonly IChapterService _chapterService;

        public MangaController(IStoryService storyService, ICommentService commentService, ICategoryService categoryService, IChapterService chapterService)
        {
            _storyService = storyService;
            _commentService = commentService;
            _categoryService = categoryService;
            _chapterService = chapterService;
        }

        public async Task<IActionResult> MangaDetail(int storyId)
        {
            var responseCt = await _categoryService.GetAllCategoriesAsync<ResponseDto>(null);
            var responseC = await _commentService.GetAllAsync<ResponseDto>(storyId, null);
            var responseM = await _storyService.GetStoryByIdAsync<ResponseDto>(storyId, null);
            if (responseM.IsSuccess && responseC.IsSuccess && responseCt.IsSuccess)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseCt.Result));
                var comments = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(Convert.ToString(responseC.Result));
                var manga = JsonConvert.DeserializeObject<StoryDTO>(Convert.ToString(responseM.Result));
                ViewBag.Categories = categories;
                ViewBag.Manga = manga;
                ViewBag.Comments = comments;
                return View("Views/Manga/MangaDetail.cshtml");
            }
            return View("Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> SearchManga(int categoryId)
        {
            var responseC = await _categoryService.GetAllCategoriesAsync<ResponseDto>(null);
            var responseM = await _storyService.GetStoryByCategoryId<ResponseDto>(categoryId, null);
            if (responseM.IsSuccess && responseC.IsSuccess)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseC.Result));
                var stories = JsonConvert.DeserializeObject<IEnumerable<StoryDTO>>(Convert.ToString(responseM.Result));
                ViewBag.Categories = categories;
                ViewBag.Stories = stories;
                return View("Views/Manga/SearchManga.cshtml");
            }
            return View("Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> ReadingManga(int storyId, int index)
        {
            //if (chapterId == 0)
            //{
            //    chapterId = 1;
            //}

            if (index == 0)
            {
                index = 1;
            }
            var responseCt = await _categoryService.GetAllCategoriesAsync<ResponseDto>(null);
            var responseM = await _storyService.GetStoryByIdAsync<ResponseDto>(storyId, null);
            var responseC = await _chapterService.GetChapterByIndexAsync<ResponseDto>(index,storyId, null);
            if (responseM.IsSuccess && responseCt.IsSuccess && responseC.IsSuccess)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseCt.Result));
                var story = JsonConvert.DeserializeObject<StoryDTO>(Convert.ToString(responseM.Result));
                var chapter = JsonConvert.DeserializeObject<ChapterDTO>(Convert.ToString(responseC.Result));
                ViewBag.Categories = categories;
                ViewBag.StoryDetail = story;
                ViewBag.Chapter = chapter;
                ViewBag.Index = index;
                return View("Views/Manga/ReadManga.cshtml");
            }
            return View("Views/Shared/Error.cshtml");
        }
    }
}

