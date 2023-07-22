using DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Dtos;

namespace StoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        protected ResponseDto _response;
        private IFavouriteRepository _favouriteRepository;

        public FavouriteController(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
            _response = new ResponseDto();
        }

        [HttpGet("GetFavourites/{userId}")]
        [Authorize]
        public async Task<object> GetFavourties(int userId)
        {
            try
            {
                var result = await _favouriteRepository.GetFavourites(userId);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("AddFavourite")]
        [Authorize]
        public async Task<object> AddFavourite([FromBody] FavouriteDTO favourite)
        {
            try
            {
                var result = await _favouriteRepository.AddFavourite(favourite);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost("DeleteFavourite")]
        [Authorize]
        public async Task<object> DeleteFavourite(FavouriteDTO favourite)
        {
            try
            {
                var result = await _favouriteRepository.RemoveFavourite(favourite);
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
