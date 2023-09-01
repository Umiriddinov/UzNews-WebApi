using Dapper;
using UzNews.WebApi.DataAcces.Interfaces.TagsFolder;
using UzNews.WebApi.Domain.Entities.TagsFolder;

namespace UzNews.WebApi.DataAcces.Repositories.TagsFolder;

public class TagsRepository : BaseRepository, ITagsRepository
{
    public async Task<int> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM tags ;";
            var result = await _connection.QuerySingleAsync<int>(query);

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

    public async Task<long> CreateAsync(Tags entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO tags (tag, created_at, updated_at) " +
                        "VALUES (@Tag, @CreatedAt, @UpdatedAt) RETURNING id;";
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
            string query = $"DELETE FROM tags WHERE id={id}";
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

    public async Task<Tags?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM tags WHERE id = @Id";
            var data = await _connection.QuerySingleAsync<Tags>(query, new { Id = id });

            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Tags?> SearchAsync(string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM tags WHERE tag ILIKE '%{search}%' ORDER BY id DESC LIMIT 1;";
            var data = await _connection.QuerySingleAsync<Tags>(query);

            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(Tags entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE news SET tag=@Tag, " +
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
