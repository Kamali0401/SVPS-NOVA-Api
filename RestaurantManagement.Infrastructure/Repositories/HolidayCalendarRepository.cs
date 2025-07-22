using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on discount.
    /// </summary>
    public class HolidayCalendarRepository : IHolidayCalendarRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayCalendarRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing discounting data.</param>
        public HolidayCalendarRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HolidayCalendar>> GetHolidayCalendarDetails(int? id)
        {
            var spName = SPNames.SP_GETHOLIDAYCALENDAR; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<HolidayCalendar>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<HolidayCalendar> InsertHolidayCalendarDetails(HolidayCalendar holiday)
        {
            var spName = SPNames.SP_INSERTHOLIDAYCALENDAR; // Name of your stored procedure
                                                         // Define parameters for the stored procedure



            var parameters = new
            {
                DateofHoliday = holiday.DateofHoliday,
                HolidayDetails = holiday.HolidayDetails,


                CreatedBy = holiday.CreatedBy,

            };

            // Execute the stored procedure and retrieve the inserted data
            await _db.Connection.QuerySingleOrDefaultAsync<HolidayCalendar>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return holiday;


        }
        /// <inheritdoc/>
        public async Task UpdateHolidayCalendarDetails(HolidayCalendar holiday)
        {
            var spName = SPNames.SP_UPDATEHOLIDAYCALENDAR; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = holiday.Id,
                DateofHoliday = holiday.DateofHoliday,
                HolidayDetails = holiday.HolidayDetails,
                ModifiedBy = holiday.ModifiedBy,



            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteHolidayCalendarDetails(int id)
        {
            var spName = SPNames.SP_DELETEHOLIDAYCALENDAR; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
    }
}
