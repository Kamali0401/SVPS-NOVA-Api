using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on ContentLib.
    /// </summary>
    public class ContentLibRepository : IContentLibRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLibRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing ContentLibing data.</param>
        public ContentLibRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ContentLib>> GetContentLibDetails(int? id)
        {
            var spName = SPNames.SP_GETCONTENTLIBDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<ContentLib>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<ContentLib> InsertContentLibDetails(ContentLib contentLibModel)
        {
            var spName = SPNames.SP_INSERTCONTENTLIBDETAILS; // Name of your stored procedure
                                                                 // Define parameters for the stored procedure
            
            

            var parameters = new
            {
                FacultyId = contentLibModel.FacultyId,
                SectionId = contentLibModel.SectionId,
                Title = contentLibModel.Title,
                Description = contentLibModel.Description,
                ExpiryDate = contentLibModel.ExpiryDate,
                CreatedBy = contentLibModel.CreatedBy,
                CreatedDate = contentLibModel.CreatedDate

            };

            // Execute the stored procedure and retrieve the inserted data
            int? newId =await _db.Connection.QuerySingleOrDefaultAsync<int?>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            


            if (newId.HasValue)
            {
                contentLibModel.Id = newId.Value;
            }

            return contentLibModel;


        }
        /// <inheritdoc/>
        public async Task UpdateContentLibDetails(ContentLib contentLibModel)
        {
            var spName = SPNames.SP_UPDATECONTENTLIBDETAILS; // Update the stored procedure name if necessary
            

            var parameters = new
            {

                Id = contentLibModel.Id,
                SectionId = contentLibModel.SectionId,
                Title = contentLibModel.Title,
                FacultyId = contentLibModel.FacultyId,
                Description = contentLibModel.Description,
                ExpiryDate = contentLibModel.ExpiryDate,
                ModifiedBy = contentLibModel.ModifiedBy,
                ModifiedDate = contentLibModel.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteContentLibDetails(int id)
        {
            var spName = SPNames.SP_DELETECONTENTLIB; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<ContentLib>> GetAllContentLibByStudent(int student)
        {
            var spName = SPNames.SP_GETALLCONTENTLIBBYSTUDENT; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<ContentLib>(spName,
                new { studentId = student }, commandType: CommandType.StoredProcedure).ToList());
        }

    }
}
