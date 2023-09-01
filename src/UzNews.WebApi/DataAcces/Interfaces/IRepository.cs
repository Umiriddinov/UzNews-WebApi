namespace UzNews.WebApi.DataAcces.Interfaces;

public interface IRepository<TModel>
{
    public Task<int> CountAsync();
    public Task<long> CreateAsync(TModel entity);
    public Task<int> UpdateAsync(TModel entity);
    public Task<int> DeleteAsync(long id);
    public Task<TModel?> GetByIdAsync(long id);
}
