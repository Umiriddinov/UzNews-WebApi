namespace UzNews.WebApi.Domain.Entities.NewsFolder;

public class NewsTags : Auditable
{
    public long NewsId { get; set; }
    public long TagsId { get; set; }
}
