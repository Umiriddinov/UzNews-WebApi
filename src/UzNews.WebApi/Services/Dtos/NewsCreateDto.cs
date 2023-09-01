namespace UzNews.WebApi.Services.Dtos;

public class NewsCreateDto
{
    public string Title { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = default!;
    public string Content { get; set; } = string.Empty;
    public IList<long> TagIds { get; set; } = new List<long>();
    public IList<string> NewTags { get; set; } = new List<string>();
}
