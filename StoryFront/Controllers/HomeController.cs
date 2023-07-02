using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StoryFront.Helpers;
using StoryFront.Models;

namespace StoryFront.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
    }

    public IActionResult Index()
    {
        FirebaseService.DeleteImage("https://firebasestorage.googleapis.com/v0/b/fir-react-87033.appspot.com/o/Checked%2FAnother%2Fdb36f72d-ae1c-42ee-8a3c-f998ec1b3e12.jpg?alt=media", "Checked/Another");
        return View();
    }
}

