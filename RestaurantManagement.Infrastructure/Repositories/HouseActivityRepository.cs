using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;
using SonaNova.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class HouseActivityRepository : IHouseActivityRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HouseActivityRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public HouseActivityRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HouseActivity>> GetHouseActivity(int? id)
        {
            var spName = SPNames.SP_GETHOUSEACTIVITY; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<HouseActivity>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<HouseActivity> InsertHouseActivity(HouseActivity houseActivity)
        {
            var spName = SPNames.SP_INSERTHOUSEACTIVITY; // Name of your stored procedure
                                                                 // Define parameters for the stored procedure
            
            

            var parameters = new
            {

                ActivityName = houseActivity.ActivityName,
                HouseId = houseActivity.HouseId,
                Point = houseActivity.Point,

                StudentList = houseActivity.StudentList,
                CreatedBy = houseActivity.CreatedBy,
                CreatedDate = houseActivity.CreatedDate,
            };

            // Execute the stored procedure and retrieve the inserted data
             await _db.Connection.QuerySingleOrDefaultAsync<HouseActivity>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return houseActivity;


        }
        /// <inheritdoc/>
        public async Task UpdateHouseActivity(HouseActivity houseActivity)
        {
            var spName = SPNames.SP_UPDATEHOUSEACTIVITY; // Update the stored procedure name if necessary
            

            var parameters = new
            {
                ActivityName = houseActivity.ActivityName,
                HouseId = houseActivity.HouseId,
                StudentList = houseActivity.StudentList,
                Point = houseActivity.Point,
                Id = houseActivity.Id,
                ModifiedBy = houseActivity.ModifiedBy,
                ModifiedDate = houseActivity.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<string> DeleteHouseActivityDetails(int id)
        {
            var spName = SPNames.SP_DELETEHOUSEACTIVITY; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return "Success";
        }

        public async Task<IEnumerable<HousePointModel>> GetHousePointDetails()
        {
            var spName = SPNames.SP_GETHOUSEPOINT; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<HousePointModel>(spName,
                commandType: CommandType.StoredProcedure).ToList());
        }
    }
}
