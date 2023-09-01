namespace UzNews.WebApi.Services.Dtos;

public class NewsUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}
