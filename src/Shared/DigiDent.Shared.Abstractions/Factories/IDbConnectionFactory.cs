using System.Data;

namespace DigiDent.Shared.Abstractions.Factories;

public interface IDbConnectionFactory<out TConnection>
    where TConnection: IDbConnection
{
    TConnection CreateConnection();
    TConnection CreateOpenConnection();
}