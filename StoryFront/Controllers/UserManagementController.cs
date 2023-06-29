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
            if (ModelState.IsValid)
            {
                //var token = HttpContext.Session.GetString("token");
                //if (token == null)
                //{
                //    return NotFound();
                //}
                var lastestUser = await _userService.GetAllUsersAsync<ResponseDto>("");
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
                userDto.ImageUser = downloadUrl;
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
        public IActionResult UserEdit(UserDTO userDto)
        {

            return View();
        }

        public IActionResult UserDelete()
        {
            return View();
        }
    }
}

