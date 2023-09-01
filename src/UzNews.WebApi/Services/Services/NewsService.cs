using AutoMapper;
using UzNews.WebApi.DataAcces.Interfaces.NewsFolder;
using UzNews.WebApi.DataAcces.Interfaces.TagsFolder;
using UzNews.WebApi.DataAcces.ViewModels;
using UzNews.WebApi.Domain.Entities.NewsFolder;
using UzNews.WebApi.Domain.Entities.TagsFolder;
using UzNews.WebApi.Domain.Exceptions.Files;
using UzNews.WebApi.Domain.Exceptions.NewsFolder;
using UzNews.WebApi.Helpers;
using UzNews.WebApi.Services.Dtos;
using UzNews.WebApi.Services.Interfaces;
using UzNews.WebApi.Services.Interfaces.Common;
using UzNews.WebApi.Utils;

namespace UzNews.WebApi.Services.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;
    private readonly ITagsRepository _tagsRepository;
    private readonly INewsTagsRepository _newsTagsRepository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;
    private readonly IMapper _mapper;

    public NewsService(INewsRepository newsRepository,
        ITagsRepository tagsRepository,
        INewsTagsRepository newsTagsRepository,
        IFileService fileService,
        IPaginator paginator,
        IMapper mapper)
    {
        this._newsRepository = newsRepository;
        this._tagsRepository = tagsRepository;
        this._newsTagsRepository = newsTagsRepository;
        this._fileService = fileService;
        this._paginator = paginator;
        this._mapper = mapper;
    }

    public async Task<bool> CreateAsync(NewsCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image);
        var news = _mapper.Map<News>(dto);
        news.CreatedAt = news.UpdatedAt = TimeHelper.GetDateTime();
        news.ImagePath = imagepath;
        var newsId = await _newsRepository.CreateAsync(news);
        if(dto.NewTags.Count > 0)
        {
            foreach(var tag in dto.NewTags)
            {
                var tags = new Tags();
                tags.Tag = tag;
                tags.CreatedAt = tags.UpdatedAt = TimeHelper.GetDateTime();
                var tagsId = await _tagsRepository.CreateAsync(tags);
                dto.TagIds.Add(tagsId);
            }
        }
        if(dto.TagIds.Count > 0)
        {
            foreach (var tagId in dto.TagIds)
            {
                var newsTags = new NewsTags();
                newsTags.TagsId = tagId;
                newsTags.NewsId = newsId;
                newsTags.CreatedAt = newsTags.UpdatedAt = TimeHelper.GetDateTime();
                var result = await _newsTagsRepository.CreateAsync(newsTags);
            }
        }

        return true;
    }

    public async Task<bool> DeleteAsync(long Id)
    {
        var news = await _newsRepository.GetByIdAsync(Id);
        if (news is null) throw new NewsNotFoundException();
        var result = await _fileService.DeleteImageAsync(news.ImagePath);
        if (result == false) throw new ImageNotFoundException();
        var dbResult = await _newsRepository.DeleteAsync(Id);

        return dbResult > 0;
    }

    public async Task<IList<NewsViewModel>> GetAllAsync(PaginationParams @params)
    {
        var news = await _newsRepository.GetAllAsync(@params);
        var count = await _newsRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return news;
    }

    public async Task<IList<NewsViewModel>> SearchAsync(PaginationParams @params, string search)
    {
        var news = await _newsRepository.SearchAsync(@params, search);
        var count = await _newsRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return news;
    }

    public async Task<bool> UpdateAsync(long Id, NewsUpdateDto dto)
    {
        var news = await _newsRepository.GetByIdAsync(Id);
        if (news is null) throw new NewsNotFoundException();
        news.Title = dto.Title;
        news.Content = dto.Content;

        if (dto.Image is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(news.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();
            string newImagePath = await _fileService.UploadImageAsync(dto.Image);
            news.ImagePath = newImagePath;
        }

        news.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _newsRepository.UpdateAsync(news);

        return dbResult > 0;
    }
}
