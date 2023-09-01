namespace UzNews.WebApi.Domain.Entities.NewsFolder;

public class News : Auditable
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
}
