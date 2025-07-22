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
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public ActivityRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Activity>> GetActivityData(int? id)
        {
            var spName = SPNames.SP_GETACTIVITYDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Activity>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Activity> InsertActivityData(Activity activityData)
        {
            var spName = SPNames.SP_INSERTACTIVITYDETAILS; // Name of your stored procedure
                                                          // Define parameters for the stored procedure
            List<string> lst = new List<string>();
            if (activityData.FilePath != "" && activityData.FilePath != null)
            {
                string[] filePaths = Directory.GetFiles(activityData.FilePath);
                foreach (var file in filePaths)
                {
                    lst.Add(Path.GetFileName(file));
                    Console.WriteLine(file);
                }

                activityData.Files = lst;
            }


            var parameters = new
            {
                ActivityID = activityData.ActivityID,

                Data = activityData.Data,
                DepartmentID = activityData.DepartmentID,
                //FilePath = activityData.FilePath,
                CreatedBy = activityData.CreatedBy,
                CreatedDate = activityData.CreatedDate

            };

            // Execute the stored procedure and retrieve the inserted data
            /* await _db.Connection.QuerySingleOrDefaultAsync<Activity>(
                 spName,
                 parameters,
                 commandType: CommandType.StoredProcedure
             );*/
            int? newStudentId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
                   spName,
                   parameters,
                   commandType: CommandType.StoredProcedure
                );


            if (newStudentId.HasValue)
            {
                activityData.Id = newStudentId.Value;
            }


            return activityData;


        }
        /// <inheritdoc/>
        public async Task UpdateActivityData(Activity activityData)
        {
            var spName = SPNames.SP_UPDATEACTIVITYDETAILS; // Update the stored procedure name if necessary
            

            var parameters = new
            {
                Id = activityData.Id,
                ActivityID = activityData.ActivityID,

                Data = activityData.Data,
                DepartmentID = activityData.DepartmentID,
                //FilePath = activityData.FilePath,
                //Files = activityData.Files,
                ModifiedBy = activityData.ModifiedBy,
                ModifiedDate = activityData.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteActivityData(int id)
        {
            var spName = SPNames.SP_DELETEACTIVITYDETAILS; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }

        public async Task<IEnumerable<Activity>> GetAllActivityData(int Type, long? DepartmentId)
        {
            var spName = SPNames.SP_GETAllACTIVITYDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Activity>(spName,
                new { Id = Type, DepartmentID = DepartmentId }, commandType: CommandType.StoredProcedure).ToList());
        }
    }
}
