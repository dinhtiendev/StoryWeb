﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StoryFront.Models;

namespace StoryFront.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
