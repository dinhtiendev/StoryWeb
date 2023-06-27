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

            // Create a unique filename for the image (optional)
            string filename = "image_7.jpg";

            // Path to the local image file
            string imagePath = "/Users/admin/Documents/PRN231/SourceImages/BaekXX/es6/image_7.jpg";

            // Open a stream for the local image file
            FileStream stream = new FileStream(imagePath, FileMode.Open);

            // Upload the image file to Firebase Storage
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

