using Npgsql;

namespace UzNews.WebApi.DataAcces.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=uznews_db; User Id=postgres; Password=@20112606;");
    }
}
