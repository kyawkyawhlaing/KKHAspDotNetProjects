using System.Data;
using Npgsql;

namespace NTier.Database.Data;

public sealed class DbConnectionFactory(NpgsqlDataSource dataSource)
{
    public IDbConnection GetOpenConnection()
    {
        NpgsqlConnection connection = dataSource.OpenConnection();

        return connection;
    }
}
