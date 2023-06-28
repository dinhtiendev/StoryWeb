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
                config.CreateMap<Chapter, ChapterDTO>()
                      .ForMember(dest => dest.ChapterId, opt => opt.MapFrom(src => src.ChapterId))
                      .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.Index))
                      .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
                      .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt))
                      .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.StoryId))
                      .ForMember(dest => dest.ListOfImage, opt => opt.MapFrom(src => MapImageToDto(src.Images)));
                config.CreateMap<ChapterDTO, Chapter>()
                      .ForMember(dest => dest.ChapterId, opt => opt.MapFrom(src => src.ChapterId))
                      .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.Index))
                      .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
                      .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt))
                      .ForMember(dest => dest.StoryId, opt => opt.MapFrom(src => src.StoryId))
                      .ForMember(dest => dest.Images, opt => opt.MapFrom(src => MapDtoToImage(src.ListOfImage)));

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

        private static ICollection<Image> MapDtoToImage(List<ImageDTO> listOfImage)
        {
            var images = new List<Image>();

            foreach (var imageDto in listOfImage)
            {
                var chapter = new Image
                {
                    ImageId = imageDto.ImageId,
                    Index = imageDto.Index,
                    ImageChapter = imageDto.ImageChapter,
                    ChapterId = imageDto.ChapterId
                };

                images.Add(chapter);
            }

            return images;
        }

        private static List<ImageDTO> MapImageToDto(ICollection<Image> images)
        {
            var listOfImage = new List<ImageDTO>();

            foreach (var image in images)
            {
                var chapterDto = new ImageDTO
                {
                    ImageId = image.ImageId,
                    Index = image.Index,
                    ImageChapter = image.ImageChapter,
                    ChapterId = image.ChapterId
                };
                listOfImage.Add(chapterDto);
            }

            return listOfImage;
        }
    }
}

