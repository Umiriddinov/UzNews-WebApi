using UzNews.WebApi.Utils;

namespace UzNews.WebApi.DataAcces.Common;

public interface IGetAll<TViewModel>
{
    public Task<IList<TViewModel>> GetAllAsync(PaginationParams @params);
}
