using Dapper;
using UzNews.WebApi.DataAcces.Interfaces.NewsFolder;
using UzNews.WebApi.DataAcces.ViewModels;
using UzNews.WebApi.Domain.Entities.NewsFolder;
using UzNews.WebApi.Utils;

namespace UzNews.WebApi.DataAcces.Repositories.NewsFolder;

public class NewsRepository : BaseRepository, INewsRepository
{
    public async Task<int> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM news ;";
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

    public async Task<long> CreateAsync(News entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO news (title, content, image_path, created_at, updated_at) " +
                        "VALUES (@Title, @Content, @ImagePath, @CreatedAt, @UpdatedAt) RETURNING id;";
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
            string query = $"DELETE FROM news WHERE id={id}";
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

    public async Task<IList<NewsViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT news.id, news.title, news.content, news.image_path, news.created_at, " +
                $"news.updated_at, ARRAY_AGG(tags.tag) AS tags FROM news JOIN news_tags ON news_tags.news_id = news.id " +
                    $"JOIN tags ON tags.id = news_tags.tags_id GROUP BY news.id ORDER BY id DESC " +
                        $"OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<NewsViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<NewsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<News?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM news WHERE id = @Id";
            var data = await _connection.QuerySingleAsync<News>(query, new { Id = id });

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

    public async Task<IList<NewsViewModel>> SearchAsync(PaginationParams @params, string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT news.id, news.title, news.content, news.image_path, news.created_at, " +
                $"news.updated_at, ARRAY_AGG(tags.tag) AS tags FROM news JOIN news_tags ON news_tags.news_id = news.id " +
                    $"JOIN tags ON tags.id = news_tags.tags_id GROUP BY news.id HAVING " +
                        $"EXISTS ( SELECT 1 FROM unnest(ARRAY_AGG(tags.tag)) as tag WHERE tag ILIKE '%{search}%' ) " +
                            $"ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";
            var result = (await _connection.QueryAsync<NewsViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<NewsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(News entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE news SET title=@Title, content=@Content, image_path=@ImagePath, " +
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
