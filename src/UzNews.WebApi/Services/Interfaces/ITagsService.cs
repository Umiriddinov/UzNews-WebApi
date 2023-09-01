using UzNews.WebApi.Domain.Entities.TagsFolder;

namespace UzNews.WebApi.Services.Interfaces;

public interface ITagsService
{
    public Task<Tags?> SearchAsync(string search);
}
