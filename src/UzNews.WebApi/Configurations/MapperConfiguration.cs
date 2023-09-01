using AutoMapper;
using UzNews.WebApi.Domain.Entities.NewsFolder;
using UzNews.WebApi.Services.Dtos;

namespace UzNews.WebApi.Configurations;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CreateMap<NewsCreateDto, News>().ReverseMap();
    }
}
