
using Microsoft.Data.SqlClient;

namespace DigiDent.Shared.Infrastructure.Persistence.Factories;

public class SqlConnectionFactory
{
    private readonly string _connectionString;
    
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
    
    public SqlConnection CreateOpenConnection()
    {
        var connection = CreateConnection();
        connection.Open();
        return connection;
    }
}