using System;
using ObjectModel.Dtos;

namespace StoryFront.Services.IServices
{
	public interface IStoryService
    {
        Task<T> GetAllStorysAsync<T>(string token);
        Task<T> GetStoryByIdAsync<T>(int id, string token);
        Task<T> CreateStoryAsync<T>(StoryDTO storyDto, string token);
        Task<T> UpdateStoryAsync<T>(StoryDTO storyDto, string token);
        Task<T> DeleteUserAsync<T>(int id, string token);
    }
}

