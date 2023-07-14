using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Dtos;

namespace StoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        protected ResponseDto _response;
        private IStoryRepository _storyRepository;
        private IChapterRepository _chapterRepository;

        public StoryController(IStoryRepository storyRepository, IChapterRepository chapterRepository)
        {
            _storyRepository = storyRepository;
            _chapterRepository = chapterRepository;
            this._response = new ResponseDto();
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<StoryDTO> storyDtos = await _storyRepository.GetStories();
                _response.Result = storyDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetTop4Trending")]
        public async Task<object> GetTop4Trending()
        {
            try
            {
                IEnumerable<StoryDTO> storyDtos = await _storyRepository.GetTop4Trending();
                _response.Result = storyDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetTop10Popular")]
        public async Task<object> GetTop10Popular()
        {
            try
            {
                IEnumerable<StoryDTO> storyDtos = await _storyRepository.GetTop10Popular();
                _response.Result = storyDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                StoryDTO storyDto = await _storyRepository.GetStoryById(id);
                _response.Result = storyDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<object> GetById(int categoryId)
        {
            try
            {
                IEnumerable<StoryDTO> storyDtos = await _storyRepository.GetStoriesByCategory(categoryId);
                _response.Result = storyDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] StoryDTO storyDto)
        {
            try
            {
                if (await _storyRepository.GetStoryById(storyDto.StoryId) == null)
                {
                    StoryDTO model = await _storyRepository.CreateStory(storyDto);
                    _response.Result = model;
                }
                else
                {
                    throw new Exception("This story is exist");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPut]
        public async Task<object> Put([FromBody] StoryDTO storyDto)
        {
            try
            {
                if (await _storyRepository.GetStoryById(storyDto.StoryId) != null)
                {
                    StoryDTO model = await _storyRepository.UpdateStory(storyDto);
                    _response.Result = model;
                }
                else
                {
                    throw new Exception("This story is NOT exist");
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                if (await _storyRepository.GetStoryById(id) != null)
                {
                    bool isSuccess = await _storyRepository.DeleteStory(id);
                    _response.Result = isSuccess;
                    if (!isSuccess)
                    {
                        throw new Exception("Can't delete");
                    }
                }
                else
                {
                    throw new Exception("This story is NOT exist");
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
