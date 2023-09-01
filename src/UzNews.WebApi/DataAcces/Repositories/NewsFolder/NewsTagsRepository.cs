using Dapper;
using UzNews.WebApi.DataAcces.Interfaces.NewsFolder;
using UzNews.WebApi.Domain.Entities.NewsFolder;

namespace UzNews.WebApi.DataAcces.Repositories.NewsFolder;

public class NewsTagsRepository : BaseRepository, INewsTagsRepository
{
    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<long> CreateAsync(NewsTags entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO news_tags (news_id, tags_id, created_at, updated_at) " +
                        "VALUES (@NewsId, @TagsId, @CreatedAt, @UpdatedAt) RETURNING id;";
            var result = await _connection.ExecuteScalarAsync(query, entity);

            return (long)result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM news_tags WHERE id={id}";
            var result = await _connection.ExecuteAsync(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<NewsTags?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(NewsTags entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE news SET news_id=@NewsId, tags_id=@TagsId, " +
                $"updated_at=@UpdatedAt WHERE id = {entity.Id};";

            return await _connection.ExecuteAsync(query, entity);
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
