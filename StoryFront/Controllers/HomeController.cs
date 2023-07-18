using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Helpers;
using StoryFront.Models;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers;

public class HomeController : Controller
{
    private readonly IStoryService _storyService;
    private readonly ICategoryService _categoryService;

    public HomeController(IStoryService storyService,ICategoryService categoryService)
    {
        _storyService = storyService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var responseC = await _categoryService.GetAllCategoriesAsync<ResponseDto>("");
        var response4 = await _storyService.GetTop4TrendingAsync<ResponseDto>("");
        var response10 = await _storyService.GetTop10PopularAsync<ResponseDto>("");

        var checkRespose = responseC.IsSuccess && response4.IsSuccess && response10.IsSuccess;
        if (checkRespose)
        {
            var top4 = JsonConvert.DeserializeObject<IEnumerable<StoryDTO>>(Convert.ToString(response4.Result));
            var top10 = JsonConvert.DeserializeObject<IEnumerable<StoryDTO>>(Convert.ToString(response10.Result));
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseC.Result));
            ViewBag.Categories = categories;
            ViewBag.Top4 = top4;
            ViewBag.Top10 = top10;
            return View("Views/Home/Index.cshtml");
        }
        return View("Views/Shared/Error.cshtml");
    }
}

