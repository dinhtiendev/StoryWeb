using System;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
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
                FirebaseStorage storage = new FirebaseStorage("fir-react-87033.appspot.com");
                string filename = Guid.NewGuid() + ".jpg";
                var stream = userDto.File.OpenReadStream();
                var task = await storage.Child("Users")
                                  .Child(filename)
                                  .PutAsync(stream);
                var downloadUrl = await storage.Child("Users").Child(filename).GetDownloadUrlAsync();
                UriBuilder uriBuilder = new UriBuilder(downloadUrl.ToString());
                Uri uri = uriBuilder.Uri;
                var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
                queryParams.Remove("token");
                uriBuilder.Query = queryParams.ToString();
                string userImage = uriBuilder.ToString();
                userDto.ImageUser = userImage;
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
                    var imageUser = userDto.ImageUser;
                    UriBuilder uriBuilder = new UriBuilder(imageUser.ToString());
                    Uri uri = uriBuilder.Uri;
                    string nameImage = System.IO.Path.GetFileName(uri.LocalPath);
                    FirebaseStorage storage = new FirebaseStorage("fir-react-87033.appspot.com");
                    var stream = userDto.File.OpenReadStream();
                    var task = await storage.Child("Users")
                                      .Child(nameImage)
                                      .PutAsync(stream);
                    string userImage = uriBuilder.ToString();
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

