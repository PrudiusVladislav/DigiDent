using Dapper;
using DigiDent.InventoryManagement.Persistence.Constants;
using Microsoft.Data.SqlClient;

namespace DigiDent.InventoryManagement.Persistence.Shared;

internal class SharedRepository
{
    private const string Schema = ConfigurationConstants.InventoryManagementSchema;
    
    internal static async Task<bool> ExistsAsync(string tableName, object id, SqlConnection connection)
    {
        var query = $@"
            SELECT 
                COUNT(*)
            FROM {Schema}.[{tableName}]
            WHERE Id = @Id";
        
        int count = await connection.ExecuteScalarAsync<int>(
            query, param: new { Id = id });
        return count > 0;
    }
}