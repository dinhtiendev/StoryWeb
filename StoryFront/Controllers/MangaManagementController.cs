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
                var listIndexCategory = Enumerable.Range(0, storyDto.ListOfCheckedCategory.Count).Where(i => storyDto.ListOfCheckedCategory[i]).ToList();
                var listCategory = new List<CategoryDTO>();
                foreach (var category in listIndexCategory)
                {
                    var cate = new CategoryDTO
                    {
                        CategoryId = category + 1
                    };
                    listCategory.Add(cate);
                }
                storyDto.ListOfCategory = listCategory;
                storyDto.ImageStory = await FirebaseService.CreateImage(storyDto.FileHeader, storyDto.Title.Replace(" ", ""));
                storyDto.FileHeader = null;
                if (storyDto.ListOfChapter.Count > 0)
                {
                    for (int i = 0; i < storyDto.ListOfChapter.Count; i++)
                    {
                        var listOfImage = new List<ImageDTO>();
                        for (int j = 0; j < storyDto.ListOfChapter[i].ListOfFile.Count; j++)
                        {
                            var image = new ImageDTO
                            {
                                Index = j + 1,
                                ImageChapter = await FirebaseService.CreateImage(storyDto.ListOfChapter[i].ListOfFile[j], storyDto.Title.Replace(" ", ""), "es" + (i + 1))
                            };
                            listOfImage.Add(image);
                        }
                        storyDto.ListOfChapter[i].Index = i + 1;
                        storyDto.ListOfChapter[i].ListOfImage = listOfImage;
                        storyDto.ListOfChapter[i].ListOfFile = null;
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

