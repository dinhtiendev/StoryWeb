using AutoMapper;
using DataAccess.DbContexts;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using ObjectModel.Dtos;
using StoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public StoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoryDTO>> GetStories()
        {
            IEnumerable<Story> stories = await _context.Stories
                .Include(x => x.Chapters)
                .ThenInclude(chapter => chapter.Images)
                .Include(x => x.StoryCategories)
                .ThenInclude(category => category.Category)
                .OrderByDescending(x => x.StoryId).ToListAsync();
            return _mapper.Map<List<StoryDTO>>(stories);
        }

        public async Task<IEnumerable<StoryDTO>> GetStoriesByCategory(int categoryId)
        {
            IEnumerable<Story> stories = await _context.StoryCategories.Include(x => x.Story)
                .ThenInclude(story => story.Chapters)
                .Where(x => x.CategoryId == categoryId).Select(x => x.Story).ToListAsync();
            return _mapper.Map<List<StoryDTO>>(stories);
        }

        public async Task<IEnumerable<StoryDTO>> GetStoriesByName(string name)
        {
            IEnumerable<Story> stories = await _context.Stories.Include(x => x.Chapters).Where(x => x.Title.Contains(name)).OrderByDescending(x => x.StoryId).ToListAsync();
            return _mapper.Map<List<StoryDTO>>(stories);
        }

        public async Task<StoryDTO> GetStoryById(int storyId)
        {
            Story story = await _context.Stories.Include(x => x.Chapters).FirstOrDefaultAsync(x => x.StoryId == storyId);
            return _mapper.Map<StoryDTO>(story);
        }

        public async Task<StoryDTO> CreateStory(StoryDTO storyDto)
        {
            Story story = _mapper.Map<StoryDTO, Story>(storyDto);
            story.CreateAt = DateTime.Now;
            story.IsActive = true;
            _context.Stories.Add(story);
            await _context.SaveChangesAsync();
            return _mapper.Map<Story, StoryDTO>(story);
        }

        public async Task<StoryDTO> UpdateStory(StoryDTO storyDto)
        {
            Story oldStory = await _context.Stories.FirstOrDefaultAsync(x => x.StoryId == storyDto.StoryId);
            oldStory.Title = storyDto.Title;
            oldStory.AuthorName = storyDto.AuthorName;
            oldStory.Content = storyDto.Content;
            oldStory.ImageStory = storyDto.ImageStory;
            oldStory.IsActive = storyDto.IsActive;
            _context.Stories.Update(oldStory);
            await _context.SaveChangesAsync();
            return _mapper.Map<Story, StoryDTO>(oldStory);
        }

        public async Task<bool> DeleteStory(int storyId)
        {
            Story story = await _context.Stories.FirstOrDefaultAsync(x => x.StoryId == storyId);
            if (story == null)
            {
                return false;
            }
            story.IsActive = false;
            _context.Stories.Update(story);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
