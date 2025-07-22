using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Repositories
{/// <summary>
 /// Repository class for performing CRUD operations on TableDetails.
 /// </summary>
    public class FacultyRepository : IFacultyRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacultyRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public FacultyRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Faculty>> GetFacultyDetails(int? id)
        {
            var spName = SPNames.SP_GETFACULTY; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Faculty>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<IEnumerable<FacultyDropdown>> GetFacultyByName(string facultyName)
        {
            var spName = SPNames.SP_GETFACULTYBYNAME;
            var result = await Task.Factory.StartNew(() =>
                _db.Connection.Query<FacultyDropdown>(spName,
                    new { FacultyName = facultyName }, commandType: CommandType.StoredProcedure).ToList());

            return result;
        }


        public async Task<Faculty> InsertFaculty(Faculty facultyDetails)
        {
            var spName = SPNames.SP_INSERTFACULTY; // Name of your stored procedure
                                                             // Define parameters for the stored procedure



            var parameters = new
            {
                UserName = facultyDetails.UserName,
                Password = facultyDetails.Password,
                RoleId = facultyDetails.RoleId,
                FacultyId = facultyDetails.FacultyId,
                Faculty_FirstName = facultyDetails.Faculty_FirstName,
                Faculty_MiddleName = facultyDetails.Faculty_MiddleName,
                Faculty_LastName = facultyDetails.Faculty_LastName,
                Gender = facultyDetails.Gender,
                DOB = facultyDetails.DOB,
                FacultyMobileNo_1 = facultyDetails.FacultyMobileNo_1,
                FacultyMobileNo_2 = facultyDetails.FacultyMobileNo_2,
                Email = facultyDetails.Email,
                FilePath = facultyDetails.FilePath,
                FileNames = facultyDetails.FileNames,
                BloodGroup = facultyDetails.BloodGroup,
                Address = facultyDetails.Address,
                CreatedBy = facultyDetails.CreatedBy,
                CreatedDate = facultyDetails.CreatedDate,
                ModifiedBy = facultyDetails.ModifiedBy,
                ModifiedDate = facultyDetails.ModifiedDate


            };
           /* int? newStudentId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
               spNameInsertOrderDetails,
               parameters,
               commandType: CommandType.StoredProcedure
            );
           */
            // Execute the stored procedure and retrieve the inserted data
            int? newFacultyId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
            
);

           


            if (newFacultyId.HasValue)
            {
                facultyDetails.Id = newFacultyId.Value;
            }



            return facultyDetails;


        }
        /// <inheritdoc/>
        public async Task UpdateFaculty(Faculty facultyDetails)
        {
            var spName = SPNames.SP_UPDATEFACULTY; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = facultyDetails.Id,
                UserName = facultyDetails.UserName,
                Password = facultyDetails.Password,
                RoleId = facultyDetails.RoleId,
                FacultyId = facultyDetails.FacultyId,
                Faculty_FirstName = facultyDetails.Faculty_FirstName,
                Faculty_MiddleName = facultyDetails.Faculty_MiddleName,
                Faculty_LastName = facultyDetails.Faculty_LastName,
                Gender = facultyDetails.Gender,
                DOB = facultyDetails.DOB,
                FacultyMobileNo_1 = facultyDetails.FacultyMobileNo_1,
                FacultyMobileNo_2 = facultyDetails.FacultyMobileNo_2,
                Email = facultyDetails.Email,
                FilePath = facultyDetails.FilePath,
                FileNames = facultyDetails.FileNames,
                BloodGroup = facultyDetails.BloodGroup,
                Address = facultyDetails.Address,
                CreatedBy = facultyDetails.CreatedBy,
                CreatedDate = facultyDetails.CreatedDate,
                ModifiedBy = facultyDetails.ModifiedBy,
                ModifiedDate = facultyDetails.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteFaculty(int id)
        {
            var spName = SPNames.SP_DELETEFACULTY; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
    }
}
