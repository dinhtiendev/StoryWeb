using ObjectModel.Dtos;

namespace StoryFront.Services.IServices
{
    public interface IFavouriteService
    {
        Task<T> GetAllAsync<T>(string token, int userId);
        Task<T> AddFavouriteAsync<T>(string token, FavouriteDTO add);
        Task<T> DeleteFavouriteAsync<T>(string token, int userId, int storyId);
    }
}
