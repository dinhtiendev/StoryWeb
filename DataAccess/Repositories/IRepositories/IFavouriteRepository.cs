using ObjectModel.Dtos;
using StoryAPI.Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface IFavouriteRepository
    {
        Task<IEnumerable<FavouriteDTO>> GetFavourites(int userId);

        Task<bool> AddFavourite(FavouriteDTO favourite);

        Task<bool> RemoveFavourite(FavouriteDTO favourite);
    }
}
