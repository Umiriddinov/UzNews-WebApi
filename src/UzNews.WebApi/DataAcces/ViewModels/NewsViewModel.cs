using UzNews.WebApi.Domain.Entities.NewsFolder;

namespace UzNews.WebApi.DataAcces.ViewModels;

public class NewsViewModel : News
{
    public string[] Tags { get; set; } = {};
}

