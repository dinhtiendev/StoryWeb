using ObjectModel.Dtos;
using StoryFront.Services.IServices;

namespace StoryFront.Services
{
    public class FavouriteService : BaseService, IFavouriteService
    {
        private readonly IHttpClientFactory _clientFactory;

        public FavouriteService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> AddFavouriteAsync<T>(string token, FavouriteDTO add)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = add,
                Url = SD.storyAPIBase + "/api/Favourite/AddFavourite",
                AccessToken = token
            });
        }

        public async Task<T> DeleteFavouriteAsync<T>(string token, FavouriteDTO add)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.storyAPIBase + "/api/Favourite/DeleteFavourite/"+add.UserId,
                AccessToken = token
            });
        }

        public async Task<T> GetAllAsync<T>(string token, int userId)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.storyAPIBase + "/api/Favourite/GetFavourites/" + userId,
                AccessToken = token
            });
        }
    }
}
