using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class TimeTableRepository : ITimeTableRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTableRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public TimeTableRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TimeTable>> GetTimeTableDetails(int? id)
        {
            var spName = SPNames.SP_GETALLTIMETABLEDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<TimeTable>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<TimeTable> InsertTimeTableDetails(TimeTable timetableModel)
        {
            var spName = SPNames.SP_INSERTTIMETABLEDETAILS; // Name of your stored procedure
                                                                 // Define parameters for the stored procedure
            
            

            var parameters = new
            {
                Day = timetableModel.Day,
                SectionId = timetableModel.SectionId,
                HallNo = timetableModel.HallNo,
                WithEffectFrom = timetableModel.WithEffectFrom,
                Hour1 = timetableModel.Hour1,
                Hour2 = timetableModel.Hour2,
                Hour3 = timetableModel.Hour3,
                Hour4 = timetableModel.Hour4,
                Hour5 = timetableModel.Hour5,
                Hour6 = timetableModel.Hour6,
                Hour7 = timetableModel.Hour7,
                Hour8 = timetableModel.Hour8,

                CreatedBy = timetableModel.CreatedBy
            };

            // Execute the stored procedure and retrieve the inserted data
           await _db.Connection.QuerySingleOrDefaultAsync<TimeTable>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return timetableModel;


        }
        /// <inheritdoc/>
        public async Task UpdateTimeTableDetails(TimeTable timetableModel)
        {
            var spName = SPNames.SP_UPDATETIMETABLEDETAILS; // Update the stored procedure name if necessary
            

            var parameters = new
            {
                Id = timetableModel.Id,
                Day = timetableModel.Day,
                SectionId = timetableModel.SectionId,
                HallNo = timetableModel.HallNo,
                WithEffectFrom = timetableModel.WithEffectFrom,
                Hour1 = timetableModel.Hour1,
                Hour2 = timetableModel.Hour2,
                Hour3 = timetableModel.Hour3,
                Hour4 = timetableModel.Hour4,
                Hour5 = timetableModel.Hour5,
                Hour6 = timetableModel.Hour6,
                Hour7 = timetableModel.Hour7,
                Hour8 = timetableModel.Hour8,

                ModifiedBy = timetableModel.ModifiedBy

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteTimeTableDetails(int id)
        {
            var spName = SPNames.SP_DELETETIMETABLE; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
        public async Task<IEnumerable<TimeTable>> GetTimeTableBySectionId(int sectionId, string role)
        {
            var spName = SPNames.SP_GetTimeTableBySectionIdDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<TimeTable>(spName,
                new { SectionId = sectionId, Role = role }, commandType: CommandType.StoredProcedure).ToList());
        }
    }
}
