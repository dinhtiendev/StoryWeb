using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Dtos;
using System.IO;
using Firebase.Storage;

namespace StoryFront.Controllers
{
    public class ChapterManagementController : Controller
    {
        public ChapterManagementController()
        {

        }

        public IActionResult ChapterIndex(int storyId)
        {
            return View();
        }

        public IActionResult ChapterCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChapterCreate(ChapterDTO model)
        {
            //var a = model.ListOfImage[0].File;
            //FirebaseStorage storage = new FirebaseStorage("fir-react-87033.appspot.com");
            //string filename = "image_8.jpg";
            //string imagePath = "/Users/admin/Documents/PRN231/SourceImages/BaekXX/es6/image_7.jpg";
            //var stream = a.OpenReadStream();
            //var task = await storage.Child("images")
            //                  .Child(filename)
            //                  .PutAsync(stream);
            //var downloadUrl = await storage.Child("images").Child(filename).GetDownloadUrlAsync();
            //int tokenIndex = downloadUrl.IndexOf("&token=");
            //if (tokenIndex >= 0)
            //{
            //    downloadUrl = downloadUrl.Substring(0, tokenIndex);
            //}
            //var a = downloadUrl;
            return View();
        }

        public IActionResult ChapterDelete()
        {

            return View();
        }
    }
}

