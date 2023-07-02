using DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Dtos;

namespace StoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        protected ResponseDto _response;
        private ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _response = new ResponseDto();
        }

        [HttpGet("GetComments/{storyId}")]
        public async Task<object> GetComments(int storyId)
        {
            try
            {
                var result = await _commentRepository.GetComments(storyId);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetReplies/{storyId}")]
        public async Task<object> GetReplies(int storyId, int commentId)
        {
            try
            {
                var result = await _commentRepository.GetReplies(storyId, commentId);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
