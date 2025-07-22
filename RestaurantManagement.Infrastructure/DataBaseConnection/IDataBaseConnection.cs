using System.Data;

namespace RestaurantManagement.Infrastructure.DatabaseConnection
{
    public interface IDataBaseConnection
    {
        IDbConnection Connection { get; }
    }
}
