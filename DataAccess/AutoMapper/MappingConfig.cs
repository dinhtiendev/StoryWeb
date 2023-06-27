using System;
using AutoMapper;
using ObjectModel.Dtos;
using StoryAPI.Models;

namespace DataAccess.AutoMapper
{
	public class MappingConfig
	{
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDTO>();
                config.CreateMap<CategoryDTO, Category>();
                config.CreateMap<User, UserDTO>();
                config.CreateMap<UserDTO, User>();
                config.CreateMap<Story, StoryDTO>()
                      .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.StoryId))
                      .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                      .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorName))
                      .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                      .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
                      .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt))
                      .ForMember(dest => dest.ImageStory, opt => opt.MapFrom(src => src.ImageStory))
                      .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                      .ForMember(dest => dest.ListOfChapter, opt => opt.MapFrom(src => MapChapterToDto(src.Chapters)));
                config.CreateMap<StoryDTO, Story>()
                      .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.StoryId))
                      .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                      .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorName))
                      .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                      .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
                      .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt))
                      .ForMember(dest => dest.ImageStory, opt => opt.MapFrom(src => src.ImageStory))
                      .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                      .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => MapDtoToChapter(src.ListOfChapter)));
                ;
            });

            return mappingConfig;
        }

        private static ICollection<Chapter> MapDtoToChapter(List<ChapterDTO> listOfChapter)
        {
            var chapters = new List<Chapter>();

            foreach (var chapterDto in listOfChapter)
            {
                var chapter = new Chapter
                {
                    ChapterId = chapterDto.ChapterId,
                    Index = chapterDto.Index,
                    View = chapterDto.View,
                    CreateAt = chapterDto.CreateAt
                };

                chapters.Add(chapter);
            }

            return chapters;
        }

        private static List<ChapterDTO> MapChapterToDto(ICollection<Chapter> chapters)
        {
            var listOfChapter = new List<ChapterDTO>();

            foreach (var chapter in chapters)
            {
                var chapterDto = new ChapterDTO
                {
                    ChapterId = chapter.ChapterId,
                    Index = chapter.Index,
                    View = chapter.View,
                    CreateAt = chapter.CreateAt
                };
                listOfChapter.Add(chapterDto);
            }

            return listOfChapter;
        }
    }
}

