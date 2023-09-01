using UzNews.WebApi.Domain.Entities.TagsFolder;

namespace UzNews.WebApi.DataAcces.Interfaces.TagsFolder;

public interface ITagsRepository : IRepository<Tags>
{
    public Task<Tags?> SearchAsync(string search);
}
