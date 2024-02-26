using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Jojo.Data;
public interface IDataContext
{

    Task<IEnumerable<T>> LoadData<T, U> (string storeprocedure, U args);

    Task SaveData<T> (string storeprocedure, T args);
}