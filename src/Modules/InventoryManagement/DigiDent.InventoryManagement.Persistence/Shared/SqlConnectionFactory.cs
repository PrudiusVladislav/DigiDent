using DigiDent.Shared.Abstractions.Factories;
using Microsoft.Data.SqlClient;

namespace DigiDent.InventoryManagement.Persistence.Shared;

public class SqlConnectionFactory
    : IDbConnectionFactory<SqlConnection>
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