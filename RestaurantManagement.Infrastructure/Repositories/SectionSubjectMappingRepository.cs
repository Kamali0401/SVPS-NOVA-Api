using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;

using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using System.Collections;
using System.Data.SqlClient;
namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class SectionSubjectMappingRepository : ISectionSubjectMappingRepository
    {
        private readonly IDataBaseConnection _db;
       // private readonly AppSettings _appSettings;
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionSubjectMappingRepository"/> class.
        /// </summary>
        /// <param name="_db">The SectionSubjectMappingbase connection for accessing billing SectionSubjectMapping.</param>
        public SectionSubjectMappingRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SectionSubjectMapping>> GetSectionSubjectMapping(int? id)
        {
            var spName = SPNames.SP_GETALLBATCHSUBMAP; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<SectionSubjectMapping>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<SectionSubjectMapping> InsertSectionSubjectMappingDetails(SectionSubjectMapping SectionSubjectMapping)
        {
            var spName = SPNames.SP_INSERTBATCHSUBMAP; // Name of your stored procedure
                                                                 // Define parameters for the stored procedure
            var parameters = new
            {
                Name = SectionSubjectMapping.SectionName,
                SectionId = SectionSubjectMapping.SectionID,
                SubjectId = SectionSubjectMapping.SubjectID,
                FacultyID = SectionSubjectMapping.FacultyID,
                CreatedBy = SectionSubjectMapping.CreatedBy,
                CreatedDate = SectionSubjectMapping.CreatedDate
            };

            // Execute the stored procedure and retrieve the inserted SectionSubjectMapping
            await _db.Connection.QuerySingleOrDefaultAsync<SectionSubjectMapping>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );

            return SectionSubjectMapping;


        }
        /// <inheritdoc/>
        public async Task UpdateSectionSubjectMappingDetails(SectionSubjectMapping SectionSubjectMapping)
        {
            var spName = SPNames.SP_UPDATEBATCHSUBMAP; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = SectionSubjectMapping.Id,
                Name = SectionSubjectMapping.SectionName,
                SectionId = SectionSubjectMapping.SectionID,
                SubjectId = SectionSubjectMapping.SubjectID,
                FacultyID = SectionSubjectMapping.FacultyID,
                ModifiedBy = SectionSubjectMapping.ModifiedBy,
                ModifiedDate = SectionSubjectMapping.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<string> DeleteSectionSubjectMappingDetails(int id)
        {
            try
            {
                var spName = SPNames.SP_DELETEBATCHSUBMAP;

                /*await using SqlConnection sqlConnection = new SqlConnection(_appSettings.ConnectionInfo.TransactionDatabase.ToString());
                await sqlConnection.OpenAsync();

                using SqlCommand command = new SqlCommand(spName, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("Id", SqlDbType.Int).Value = id;

                await command.ExecuteNonQueryAsync();*/
              

                await Task.Factory.StartNew(() =>
                    _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));

               

                return "Success";
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                throw new Exception("Error while deleting SectionSubjectMapping.", ex);
            }
        }

        public async Task<IEnumerable<SectionSubjectMapping>> GetFacultyListBySectionIdDetails(int sectionId)
        {
            var spName = SPNames.SP_GetFacultyListBySectionIdDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<SectionSubjectMapping>(spName,
                new { SectionId = sectionId }, commandType: CommandType.StoredProcedure).ToList());
        }


    }
}
