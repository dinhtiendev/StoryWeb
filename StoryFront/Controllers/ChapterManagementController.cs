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

        public IActionResult ChapterIndex()
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
            FirebaseStorage storage = new FirebaseStorage("fir-react-87033.appspot.com");
            string filename = "image_7.jpg";
            string imagePath = "/Users/admin/Documents/PRN231/SourceImages/BaekXX/es6/image_7.jpg";
            FileStream stream = new FileStream(imagePath, FileMode.Open);
            var task = await storage.Child("images")
                              .Child(filename)
                              .PutAsync(stream);
            return View();
        }

        public IActionResult ChapterEdit()
        {
            return View();
        }

        public IActionResult ChapterDelete()
        {
            return View();
        }
    }
}

