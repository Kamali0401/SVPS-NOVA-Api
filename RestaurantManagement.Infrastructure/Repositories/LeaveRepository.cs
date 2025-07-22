using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class LeaveRepository : ILeaveRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public LeaveRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Leave>> GetLeave(string role, int? id)
        {
            var spName = SPNames.SP_GETALLLEAVE; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Leave>(spName,
                new {Role=role, Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<IEnumerable<Leave>> GetLeaveById(int? id)
        {
            var spName = SPNames.SP_GETLEAVEBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Leave>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Leave> InsertLeaveDetails(Leave model)
        {
            var spName = SPNames.SP_INSERTLEAVE; // Name of your stored procedure
                                                             // Define parameters for the stored procedure
            var parameters = new
            {

                StudentId = model.StudentId,
                LeaveType = model.LeaveType,
                Reason = model.Reason,
                DateOfLeave = model.DateOfLeave,
                CreatedBy = model.CreatedBy

            };

            // Execute the stored procedure and retrieve the inserted data
            /*await _db.Connection.QuerySingleOrDefaultAsync<Leave>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );
            */
            int? newId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );



            if (newId.HasValue)
            {
                model.Id = newId.Value;
            }

            return model;


        }
        /// <inheritdoc/>
        public async Task UpdateLeaveDetails(Leave model)
        {
            var spName = SPNames.SP_UPDATELEAVE; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = model.Id,
                StudentId = model.StudentId,
                LeaveType = model.LeaveType,
                Reason = model.Reason,
                DateOfLeave = model.DateOfLeave,
                ModifiedBy = model.ModifiedBy

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteLeaveDetails(int id)
        {
            var spName = SPNames.SP_DELETELEAVE; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
       

    }
}
