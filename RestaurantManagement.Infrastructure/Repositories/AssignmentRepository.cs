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
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public AssignmentRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Assignment>> GetAssignment(int? id)
        {
            var spName = SPNames.SP_GETALLASSIGNMENTDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Assignment>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Assignment> InsertAssignmentDetails(Assignment assignmentModel)
        {
            var spName = SPNames.SP_INSERTASSIGNMENTDETAILS; // Name of your stored procedure
                                                      // Define parameters for the stored procedure
             var parameters = new
            {

                 SectionId = assignmentModel.SectionId,
                 SubjectId = assignmentModel.SubjectId,
                 Title = assignmentModel.Title,
                 FacultyId = assignmentModel.FacultyId,
                 Description = assignmentModel.Description,
                 DueDate = assignmentModel.DueDate,
                 FileName = assignmentModel.FileName,
                 FilePath = assignmentModel.FilePath,
                 CreatedBy = assignmentModel.CreatedBy,

             };

            // Execute the stored procedure and retrieve the inserted data
            /* await _db.Connection.QuerySingleOrDefaultAsync<Assignment>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );*/
            int? newId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );



            if (newId.HasValue)
            {
                assignmentModel.Id = newId.Value;
            }

            return assignmentModel;


        }
        /// <inheritdoc/>
        public async Task UpdateAssignmentDetails(Assignment assignmentModel)
        {
            var spName = SPNames.SP_UPDATEASSIGNMENTDETAILS; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = assignmentModel.Id,
                SectionId = assignmentModel.SectionId,
                SubjectId = assignmentModel.SubjectId,
                Title = assignmentModel.Title,
                FacultyId = assignmentModel.FacultyId,
                Description = assignmentModel.Description,
                DueDate = assignmentModel.DueDate,
                FileName = assignmentModel.FileName,
                FilePath = assignmentModel.FilePath,
                ModifiedBy = assignmentModel.ModifiedBy

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteAssignmentDetails(int id)
        {
            var spName = SPNames.SP_DELETEASSIGNMENTFORM; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
        public async Task<IEnumerable<Assignment>> GetAllAssignmentByStudent(string role, int studentId)
        {
            var spName = SPNames.SP_GETALLASSIGNMENTBYSTUDENT; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Assignment>(spName,
                new { role = role, studentId = studentId }, commandType: CommandType.StoredProcedure).ToList());
        }

    }
}
