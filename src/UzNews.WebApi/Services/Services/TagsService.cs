using UzNews.WebApi.DataAcces.Interfaces.TagsFolder;
using UzNews.WebApi.Domain.Entities.TagsFolder;
using UzNews.WebApi.Services.Interfaces;

namespace UzNews.WebApi.Services.Services;

public class TagsService : ITagsService
{
    private readonly ITagsRepository _tagsRepository;

    public TagsService(ITagsRepository tagsRepository)
    {
        this._tagsRepository = tagsRepository;
    }
    public async Task<Tags?> SearchAsync(string search)
    {
        var tag = await _tagsRepository.SearchAsync(search);
        return tag;
    }
}
