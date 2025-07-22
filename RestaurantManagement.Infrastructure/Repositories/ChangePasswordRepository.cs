using Dapper;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using SonaNova.Domain.Entities;

namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class ChangePasswordRepository : IChangePasswordRepository
    {

        private readonly IDataBaseConnection _db;
        private readonly string _connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public ChangePasswordRepository(IDataBaseConnection db, IConfiguration configuration)
        {
            _db = db;
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public async Task<Faculty> GetVerifyPassword(string UserName, string Password)
        {
            var spName = SPNames.SP_GETVERIFYPASSWORD;
            return await _db.Connection.QueryFirstOrDefaultAsync<Faculty>(
        spName,
        new { UserName = UserName, Password = Password },
        commandType: CommandType.StoredProcedure);
        }


        public async Task<string> UpdateVerifyPassword(string UserName, string NewPassword, string OldPassword, long FacultyId)
        {
            var spName = SPNames.SP_UPDATEVERIFYPASSWORD;
            try
            {
                await using (SqlConnection sqlconnection =
                       new SqlConnection(_connectionString))
                {

                    sqlconnection.Open();

                    SqlCommand command = new SqlCommand(spName, sqlconnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("UserName", SqlDbType.VarChar).Value = UserName;
                    command.Parameters.Add("Oldpassword", SqlDbType.VarChar).Value = OldPassword;
                    command.Parameters.Add("Newpassword", SqlDbType.VarChar).Value = NewPassword;
                    command.Parameters.Add("FacultyId", SqlDbType.BigInt).Value = FacultyId;

                    command.ExecuteNonQuery();
                    sqlconnection.Close();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "2627")
                {
                    return "User name missing";
                }
                return ex.Message.ToString();
            }

        }

        public Task<int> PasswordReset(string userName, string password)
        {
            var spName = SPNames.SP_PASSWORDRESET;
            return Task.Factory.StartNew(() => _db.Connection.Execute(spName, new
            {
                UserName = userName,
                Password = password,
                ModifiedBy = userName,
                ModifiedDate = DateTime.Now

            }, commandType: CommandType.StoredProcedure));

        }
        public Task<List<UserModel>> GetUserDetails(string Username, string Password, string role)
        {
            var spName = SPNames.SP_GETUSERDETAILS;
            return Task.Factory.StartNew(() => _db.Connection.Query<UserModel>(spName, new
            {
                UserName = Username,
                Password = Password,
                Role = role
            }, commandType: CommandType.StoredProcedure).ToList());
        }
    }
    
}
