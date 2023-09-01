using UzNews.WebApi.DataAcces.ViewModels;
using UzNews.WebApi.Services.Dtos;
using UzNews.WebApi.Utils;

namespace UzNews.WebApi.Services.Interfaces;

public interface INewsService
{
    public Task<bool> CreateAsync(NewsCreateDto dto);
    public Task<IList<NewsViewModel>> GetAllAsync(PaginationParams @params);
    public Task<IList<NewsViewModel>> SearchAsync(PaginationParams @params, string search);
    public Task<bool> UpdateAsync(long Id, NewsUpdateDto dto);
    public Task<bool> DeleteAsync(long Id);
}
