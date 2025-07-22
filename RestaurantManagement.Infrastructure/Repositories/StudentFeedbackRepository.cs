using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections;

namespace SonaNova.Infrastructure.Repositories
{

    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class StudentFeedbackRepository : IStudentFeedbackRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentFeedbackRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public StudentFeedbackRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<StudentFeedback>> GetStudentFeedback(int? id)
        {
            var spName = SPNames.SP_GETSTUDENTFEEDBACKBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<StudentFeedback>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<StudentFeedback>> GetAllStudentFeedback(string role, int? id)
        {
            var spName = SPNames.SP_GETALLSTUDENTFEEDBACKDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<StudentFeedback>(spName,
                new { Role = role, Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<string> InsertStudentFeedbackDetails(List<StudentFeedback> feedbackList)
        {
            var spName = SPNames.SP_INSERTSTUDENTFEEDBACKDETAILS;
            var sendToDB = new ArrayList();

            foreach (var item in feedbackList)
            {
                sendToDB.Add(new
                {
                    FacultyId = item.FacultyId,
                    StudentRollNo = item.StudentRollNo,
                    Feedback = item.Feedback,
                    IsReadyToSentWhatsapp = item.IsReadyToSentWhatsapp,
                    CreatedBy = item.CreatedBy
                });
            }

            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, sendToDB.ToArray(), commandType: CommandType.StoredProcedure));

            return "success";
        }
        /// <inheritdoc/>
       

        public async Task<List<StudentFeedback>> DeleteStudentFeedbackDetails(string id)
        {
            var spName = SPNames.SP_DELETESTUDENTFEEDBACK; // Update the stored procedure name if necessary
          // return await Task.Factory.StartNew(() =>
          //      _db.Connection.Query<StudentFeedbackModel>(spName, new { Ids = id }, commandType: CommandType.StoredProcedure).ToList());
            //return true;
            /*return Task.Factory.StartNew(() =>
             _db.Connection.Query<StudentFeedback>(spName, new { Ids = id }, commandType: CommandType.StoredProcedure)
                 .ToList());*/
            return await Task.Run(() =>
       _db.Connection.Query<StudentFeedback>(spName, new { Ids = id }, commandType: CommandType.StoredProcedure)
           .ToList());
        }

    }
}
