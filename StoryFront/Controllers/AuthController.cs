using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{
	public class AuthController : Controller
	{
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public AuthController(IAuthService authService, IUserService userService, ICategoryService categoryService)
		{
            _authService = authService;
            _userService = userService;
            _categoryService = categoryService;
		}

        public async Task<IActionResult> Login()
        {
            var responseC = await _categoryService.GetAllCategoriesAsync<ResponseDto>("");
            if (responseC.IsSuccess && responseC.Result != null)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(Convert.ToString(responseC.Result));
                ViewBag.Categories = categories;
                return View();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var response = await _authService.Login<ResponseDto>(email, password);
            if (response.IsSuccess && response.Result != null)
            {
                var token = Convert.ToString(response.Result);
                HttpContext.Session.SetString("token", token);

                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var role = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserDTO userDto)
        {
            var response = await _userService.CreateUserAsync<ResponseDto>(userDto, "");
            if (response.IsSuccess)
            {
                var user = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
                var responseL = await _authService.Login<ResponseDto>(user.Email, user.Password);
                if (responseL.IsSuccess && responseL.Result != null)
                {
                    var token = Convert.ToString(responseL.Result);
                    HttpContext.Session.SetString("token", token);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewData["message"] = "This email is exist!";
            return View();
        }
    }
}

