using UzNews.WebApi.Utils;

namespace UzNews.WebApi.Services.Interfaces.Common;

public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);
}
