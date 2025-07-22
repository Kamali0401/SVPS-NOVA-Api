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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public SubjectRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Subject>> GetSubjectDetails(int? id)
        {
            var spName = SPNames.SP_GETALLSUBJECT; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Subject>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Subject> InsertSubjectDetails(Subject subject)
        {
            var spName = SPNames.SP_INSERTSUBJECT; // Name of your stored procedure
                                                                 // Define parameters for the stored procedure
            
            

            var parameters = new
            {
                SubjectShortForm = subject.SubjectShortForm,
                SubjectCode = subject.SubjectCode,
                SubjectName = subject.SubjectName,
                Grade = subject.Grade,
                CreatedBy = subject.CreatedBy

            };

            // Execute the stored procedure and retrieve the inserted data
            await _db.Connection.QuerySingleOrDefaultAsync<Subject>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return subject;


        }
        /// <inheritdoc/>
        public async Task UpdateSubjectDetails(Subject subject)
        {
            var spName = SPNames.SP_UPDATESUBJECT; // Update the stored procedure name if necessary
            

            var parameters = new
            {
                Id = subject.Id,
                SubjectShortForm = subject.SubjectShortForm,
                SubjectCode = subject.SubjectCode,
                SubjectName = subject.SubjectName,
                Grade = subject.Grade,
                ModifiedBy = subject.ModifiedBy

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<string> DeleteSubjectDetails(int id)
        {
            var spName = SPNames.SP_DELETESUBJECT; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return "Success";
        }
    }
}
