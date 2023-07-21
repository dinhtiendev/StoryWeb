using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Helpers;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public UserController( IUserService userService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Profile()
        {
            var token = HttpContext.Session.GetString("token");
            if (token != null)
            {
                var userId = CheckService.GetUserId(token);
                var responseCt = await _categoryService.GetAllCategoriesAsync<ResponseDto>(null);
                var response = await _userService.GetUserByIdAsync<ResponseDto>(userId, token);
                if (response.IsSuccess && responseCt.IsSuccess)
                {
                    var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseCt.Result));
                    ViewBag.Categories = categories;
                    var currentUser = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewBag.User = currentUser;
                    return View("Views/User/Profile.cshtml");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> ChangePasswordView(string errorMess)
        {
            var token = HttpContext.Session.GetString("token");
            if (token != null)
            {
                //if (!string.IsNullOrEmpty(errorMess))
                //{
                //    errorMess = "Error";
                //}
                ViewBag.Duplicate = errorMess;
                var userId = CheckService.GetUserId(token);
                var responseCt = await _categoryService.GetAllCategoriesAsync<ResponseDto>(null);
                var response = await _userService.GetUserByIdAsync<ResponseDto>(userId, token);
                if (response.IsSuccess && responseCt.IsSuccess)
                {
                    var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseCt.Result));
                    ViewBag.Categories = categories;
                    var currentUser = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                    ViewBag.User = currentUser;
                    return View("Views/User/ChangePassword.cshtml");
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDTO user)
        {
            var token = HttpContext.Session.GetString("token");
            if (token != null)
            {
                user.UserId = CheckService.GetUserId(token);
                user.Password = "";
                user.IsActive = true;
                if (user.File != null)
                {
                    await FirebaseService.EditImage(user.File, user.ImageUser, "Users");
                    user.File = null;
                }
                var response = await _userService.UpdateUserAsync<ResponseDto>(user, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("Profile", "User");
                }
                //if (response.Result == null)
                //{
                //    return await Profile();
                //}
            }
            return NotFound();
        }

        public async Task<IActionResult> ChangePassword(UserDTO user, string newPassword, string rePassword)
        {
            var token = HttpContext.Session.GetString("token");
            if (token != null)
            {
                var response = await _userService.UpdateUserAsync<ResponseDto>(user, token);
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return NotFound();
        }
    }
}
