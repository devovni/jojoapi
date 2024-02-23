using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Jojo;
public class DataContext(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public async Task<IEnumerable<T>> LoadData<T, U> (string storeprocedure, U args)
    {
        using SqlConnection dbCon = new(_config.GetConnectionString("default"));
        return await dbCon.QueryAsync<T>(
            storeprocedure, args, commandType: CommandType.StoredProcedure);
    }

    public Task SaveData<T> (string storeprocedure, T args)
    {
        using SqlConnection dbCon = new(_config.GetConnectionString("default"));
        return  dbCon.ExecuteAsync(storeprocedure, args, commandType: CommandType.StoredProcedure);
    }
}