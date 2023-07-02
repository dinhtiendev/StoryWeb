using System;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Helpers;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{
	public class UserManagementController : Controller
	{
        private IUserService _userService;

		public UserManagementController(IUserService userService)
		{
            _userService = userService;
		}

        public async Task<IActionResult> UserIndex()
        {
            List<UserDTO> list = new();
            //if (token == null)
            //{
            //    return NotFound();
            //}
            var response = await _userService.GetAllUsersAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<UserDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> UserCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate(UserDTO userDto)
        {
            ModelState.Remove(nameof(userDto.ImageUser));
            if (userDto.File == null)
            {
                ModelState.AddModelError(nameof(userDto.File), "File is required");
            }
            if (ModelState.IsValid)
            {
                //var token = HttpContext.Session.GetString("token");
                //if (token == null)
                //{
                //    return NotFound();
                //}
                userDto.ImageUser = await FirebaseService.CreateImage(userDto.File, "Users");
                userDto.File = null;
                var response = await _userService.CreateUserAsync<ResponseDto>(userDto, "");
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(UserIndex));
                }
            }
            return View();
        }

        public async Task<IActionResult> UserEdit(int userId)
        {
            UserDTO model = new();
            //if (token == null)
            //{
            //    return NotFound();
            //}
            var response = await _userService.GetUserByIdAsync<ResponseDto>(userId, "");
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<UserDTO>(Convert.ToString(response.Result));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(UserDTO userDto)
        {
            ModelState.Remove(nameof(userDto.Email));
            ModelState.Remove(nameof(userDto.Password));
            if (ModelState.IsValid)
            {
                if (userDto.File != null)
                {
                    await FirebaseService.EditImage(userDto.File, userDto.ImageUser, "Users");
                    userDto.File = null;
                }
                var response = await _userService.UpdateUserAsync<ResponseDto>(userDto, "");
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(UserIndex));
                }
            }
            return View();
        }

        public async Task<IActionResult> UserDelete(int userId)
        {
            var response = await _userService.DeleteUserAsync<ResponseDto>(userId, "");
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(UserIndex));
            }
            return RedirectToAction(nameof(UserIndex));
        }
    }
}

