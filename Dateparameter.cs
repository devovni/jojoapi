using Dapper;
using Npgsql;
using NpgsqlTypes;

namespace Jojo.Utils;
public class DateParameter(string value) : SqlMapper.ICustomQueryParameter
{
    readonly string _value = value;

    public void AddParameter(IDbCommand command, string name)
    {
        command.Parameters.Add(new NpgsqlParameter
        {
            ParameterName = name,
            NpgsqlDbType = NpgsqlDbType.Date,
            Value = _value
        });
    }
}