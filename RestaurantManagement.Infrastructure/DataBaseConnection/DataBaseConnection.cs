using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace RestaurantManagement.Infrastructure.DatabaseConnection
{
    public sealed class DataBaseConnection : IDataBaseConnection
    {
        public DataBaseConnection(IOptions<ConnectionStrings> connectionStrings)
        {
            Connection = new SqlConnection(connectionStrings.Value.DbConnection);
        }
        public IDbConnection Connection { get; }
    }
}
