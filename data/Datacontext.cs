using Dapper;
using System.Data;
using Npgsql;
using System.Reflection.Metadata;

namespace Jojo.Data;
public class DataContext(IConfiguration config): IDataContext
{
    private readonly IConfiguration _config = config;

    public IDbConnection DbConnection()
    {
        return  new NpgsqlConnection(_config.GetConnectionString("Default"));
    }
}