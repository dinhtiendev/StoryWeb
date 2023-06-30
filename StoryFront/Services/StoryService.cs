﻿using System;
using ObjectModel.Dtos;
using StoryFront.Services.IServices;

namespace StoryFront.Services
{
	public class StoryService : BaseService, IStoryService
	{
        private readonly IHttpClientFactory _clientFactory;

        public StoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAllStorysAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.storyAPIBase + "/api/Story",
                AccessToken = token
            });
        }

        public async Task<T> GetStoryByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.storyAPIBase + "/api/Story/" + id,
                AccessToken = token
            });
        }

        public async Task<T> CreateStoryAsync<T>(StoryDTO storyDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = storyDto,
                Url = SD.storyAPIBase + "/api/Story",
                AccessToken = token
            });
        }

        public async Task<T> UpdateStoryAsync<T>(StoryDTO storyDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = storyDto,
                Url = SD.storyAPIBase + "/api/Story",
                AccessToken = token
            });
        }

        public async Task<T> DeleteUserAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.storyAPIBase + "/api/Story/" + id,
                AccessToken = token
            });
        }
    }
}

