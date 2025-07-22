using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using SonaNova.Infrastructure.Interfaces;
using SonaNova.Domain.Entities;

namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class AcademicCalendarRepository :IAcademicCalendarRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcademicCalendarRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public AcademicCalendarRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AcademicCalendar>> GetAcademicCalendar(int? id)
        {
            var spName = SPNames.SP_GETACADEMICCALENDERBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<AcademicCalendar>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<AcademicCalendar>> GetAllAcademicCalendar(string role)
        {
            var spName = SPNames.SP_GETACADEMICCALENDER; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<AcademicCalendar>(spName,
                new { Role = role }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<AcademicCalendar> InsertAcademicCalendarDetails(AcademicCalendar academicCalender)
        {
            var spName = SPNames.SP_INSERTACADEMICCALENDER; // Name of your stored procedure
                                                 // Define parameters for the stored procedure
            var parameters = new
            {

                AcademicActivities = academicCalender.AcademicActivities,
                StartDate = academicCalender.StartDate,
                EndDate = academicCalender.EndDate,
                CreatedBy = academicCalender.CreatedBy,

            };

            // Execute the stored procedure and retrieve the inserted data
           /* await _db.Connection.QuerySingleOrDefaultAsync<AcademicCalendar>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );
           */
            // Execute the stored procedure and retrieve the inserted data
            int? newId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );



            if (newId.HasValue)
            {
                academicCalender.Id = newId.Value;
            }

            return academicCalender;


        }
        /// <inheritdoc/>
        public async Task UpdateAcademicCalendarDetails(AcademicCalendar academicCalender)
        {
            var spName = SPNames.SP_UPDATEACADEMICCALENDER; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = academicCalender.Id,
                AcademicActivities = academicCalender.AcademicActivities,
                StartDate = academicCalender.StartDate,
                EndDate = academicCalender.EndDate,
                ModifiedBy = academicCalender.ModifiedBy,
                ModifiedDate = academicCalender.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteAcademicCalendarDetails(int id)
        {
            var spName = SPNames.SP_DELETEACADEMICCALENDER; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }

    }
}
