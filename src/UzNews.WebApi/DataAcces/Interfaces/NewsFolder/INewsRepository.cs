using UzNews.WebApi.DataAcces.Common;
using UzNews.WebApi.DataAcces.ViewModels;
using UzNews.WebApi.Domain.Entities.NewsFolder;

namespace UzNews.WebApi.DataAcces.Interfaces.NewsFolder;

public interface INewsRepository : IRepository<News>,
    IGetAll<NewsViewModel>,
    ISearchable<NewsViewModel>
{}
