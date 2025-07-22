using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SonaNova.Infrastructure.Repositories
{

    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public NotificationRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Notification>> GetNotificationById(int? id)
        {
            var spName = SPNames.SP_GETNOTIFICATIONBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Notification>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Notification>> GetAllNotification(int studentId, string role)
        {
            var spName = SPNames.SP_GETNOTIFICATION; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Notification>(spName,
                new { studentId = studentId, role = role }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task UpdateNotificationDetails(Notification feedbackList)
        {
            var spName = SPNames.SP_UPDATENOTIFICATION;
          
            //  return await connection.QueryFirstOrDefaultAsync<StudentFeedbackModel>(spName, parameters, commandType: CommandType.StoredProcedure);
             await Task.Factory.StartNew(() =>
             _db.Connection.Query<Notification>(spName, new { StudentId = feedbackList.StudentId }, commandType: CommandType.StoredProcedure)
                 .ToList());
        }
        /// <inheritdoc/>


        

    }
}
