using System;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Dtos;
using StoryFront.Helpers;
using StoryFront.Services;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{ 
	public class MangaManagementController : Controller
	{
        private IStoryService _storyService;
        private ICategoryService _categoryService;

        public MangaManagementController(IStoryService storyService, ICategoryService categoryService)
		{
            _storyService = storyService;
            _categoryService = categoryService;
		}

        public async Task<IActionResult> MangaIndex()
        {
            List<StoryDTO> list = new();
            //if (token == null)
            //{
            //    return NotFound();
            //}
            var response = await _storyService.GetAllStorysAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<StoryDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> MangaCreate()
        {
            List<CategoryDTO> listCategory = new();
            var response = await _categoryService.GetAllCategoriesAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                listCategory = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
            }
            ViewData["listCategory"] = listCategory;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MangaCreate(StoryDTO storyDto)
        {
            for (int i = 0; i < storyDto.ListOfChapter.Count; i++)
            {
                string listOfImage = "ListOfChapter[" + i + "].ListOfImage";
                ModelState.Remove(listOfImage);
            }
            ModelState.Remove(nameof(storyDto.ImageStory));
            if (storyDto.FileHeader == null)
            {
                ModelState.AddModelError(nameof(storyDto.FileHeader), "File is required");
            }
            if (ModelState.IsValid)
            {
                //var token = HttpContext.Session.GetString("token");
                //if (token == null)
                //{
                //    return NotFound();
                //}

                storyDto.ImageStory = await FirebaseService.CreateImage(storyDto.FileHeader, storyDto.Title.Replace(" ", ""));
                storyDto.FileHeader = null;
                if (storyDto.ListOfChapter.Count > 0)
                {
                    for (int i = 0; i < storyDto.ListOfChapter.Count; i++)
                    {
                        for (int j = 0; j < storyDto.ListOfChapter[i].ListOfFile.Count; j++)
                        {
                            storyDto.ImageStory = await FirebaseService.CreateImage(storyDto.ListOfChapter[i].ListOfFile[j], storyDto.Title.Replace(" ", ""), "es" + (i + 1));
                            storyDto.ListOfChapter[i].ListOfFile[j] = null;
                        }
                    }
                }
                var response = await _storyService.CreateStoryAsync<ResponseDto>(storyDto, "");
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(MangaIndex));
                }
            }
            return View();
        }

        public async Task<IActionResult> MangaEdit()
        {
            return View();
        }

        public async Task<IActionResult> MangaDelete()
        {
            return View();
        }
    }
}

