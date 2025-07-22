using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class HouseRepository : IHouseRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HouseRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public HouseRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<House>> GetHouse(int? id)
        {
            var spName = SPNames.SP_GETALLHOUSE; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<House>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<House> InsertHouseDetails(House house)
        {
            var spName = SPNames.SP_INSERTHOUSE; // Name of your stored procedure
                                                      // Define parameters for the stored procedure
             var parameters = new
            {
                 Name = house.Name,
                 Is_Active = house.Is_Active,
                 CreatedBy = house.CreatedBy,

             };

            // Execute the stored procedure and retrieve the inserted data
             await _db.Connection.QuerySingleOrDefaultAsync<House>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return house;


        }
        /// <inheritdoc/>
        public async Task UpdateHouseDetails(House house)
        {
            var spName = SPNames.SP_UPDATEHOUSE; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = house.Id,
                Name = house.Name,
                Is_Active = house.Is_Active,
                ModifiedBy = house.ModifiedBy,

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<string> DeleteHouseDetails(int id)
        {
            var spName = SPNames.SP_DELETEHOUSE;
            try
            {
                await Task.Factory.StartNew(() =>
                    _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));

                return "Success";
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("REFERENCE constraint"))
                {
                    return "Delete failed due to existing reference. House is mapped with other records.";
                }

                // Log unexpected SQL error
                // _logger.LogError(ex, "Error deleting house with ID {Id}", id);
                return "Delete failed due to database error.";
            }
            catch (Exception ex)
            {
                // Log general exception
                // _logger.LogError(ex, "Unexpected error deleting house with ID {Id}", id);
                return "Unexpected error occurred.";
            }
        }

    }
}
