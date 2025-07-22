using Dapper;
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
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class ExamRepository : IExamRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public ExamRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Exam>> GetExam(int? id)
        {
            var spName = SPNames.SP_GETEXAMS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Exam>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Exam> InsertExamDetails(Exam Exam)
        {
            var spName = SPNames.SP_INSERTEXAMS; // Name of your stored procedure
                                                 // Define parameters for the stored procedure
            var parameters = new
            {
                Name = Exam.Name,
                IsActive = Exam.IsActive,
                // RoleId=roleMaster.RoleId  ,
                CreatedBy = Exam.CreatedBy,
                CreatedDate = Exam.CreatedDate,

            };

            // Execute the stored procedure and retrieve the inserted data
            await _db.Connection.QuerySingleOrDefaultAsync<Exam>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );

            return Exam;


        }
        /// <inheritdoc/>
        public async Task UpdateExamDetails(Exam Exam)
        {
            var spName = SPNames.SP_UPDATEEXAMS; // Update the stored procedure name if necessary


            var parameters = new
            {
                Name = Exam.Name,
                IsActive = Exam.IsActive,
                Id = Exam.Id,
                ModifiedBy = Exam.ModifiedBy,
                ModifiedDate = Exam.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<string> DeleteExamDetails(int id)
        {
            var spName = SPNames.SP_DELETEEXAMS; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return "Success";
        }

    }
}
