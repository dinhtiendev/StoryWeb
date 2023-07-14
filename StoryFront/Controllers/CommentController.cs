using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Dtos;
using StoryFront.Services.IServices;

namespace StoryFront.Controllers
{
    public class CommentController : Controller
    {

        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

    }
}
