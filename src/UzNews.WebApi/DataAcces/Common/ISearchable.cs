using UzNews.WebApi.Utils;

namespace UzNews.WebApi.DataAcces.Common;

public interface ISearchable<TViewModel>
{
    public Task<IList<TViewModel>> SearchAsync(PaginationParams @params, string search);
}
