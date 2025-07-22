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
using System.Reflection;
using RestaurantManagement.Domain.Entities;

namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class SectionStudentMappingRepository : ISectionStudentMappingRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionStudentMappingRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public SectionStudentMappingRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SectionStudentMapping>> GetSectionStudentMapping(int? id)
        {
            var spName = SPNames.SP_GETALLSECTIONSTUDMAP; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<SectionStudentMapping>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <inheritdoc/>
        

        public async Task<int> InsertSectionStudentMappingDetails(List<SectionStudentMapping> feedbackList)
        {
            var spName = SPNames.SP_INSERTSECTIONSTUDMAP;
            var sendToDB = new ArrayList();

            foreach (var item in feedbackList)
            {
                sendToDB.Add(new
                {

                    SectionId = item.SectionId,
                    StudentId = item.StudentId,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate
                });
            }

            return  await Task.Factory.StartNew(() =>
      _db.Connection.Execute(spName, sendToDB.ToArray(), commandType: CommandType.StoredProcedure));
        }
        /// <inheritdoc/>

        public async Task<int> UpdateSectionStudentMappingDetails(List<SectionStudentMapping> feedbackList)
        {
            var spName = SPNames.SP_UPDATESECTIONSTUDMAP;
            var sendToDB = new ArrayList();

            string sProc = SPNames.SP_UPDATESECTIONSTUDACTIVEMAP;
            var rowsUpdated = _db.Connection.Execute(sProc,
                new { SectionId = feedbackList.FirstOrDefault(x => x.SectionId != 0).SectionId },
                commandType: CommandType.StoredProcedure);
            foreach (var item in feedbackList)
            {
                sendToDB.Add(
                    new
                    {
                        Id = item.Id,
                        SectionId = item.SectionId,
                        StudentId = item.StudentId,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedDate = item.ModifiedDate
                    });

            }

            return await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, sendToDB.ToArray(), commandType: CommandType.StoredProcedure));
        }
        public async Task<int> DeleteSectionStudentMappingDetails(int[] ids, int batchId)
        {
            var delRecIds = new ArrayList();
            foreach (int id in ids)
            {
                delRecIds.Add(
                    new
                    {
                        Id = id
                    });
            }

            var spName1 = SPNames.SP_DELETESECTIONSTUDMAP;
            var spName = SPNames.SP_UPDATESECTIONSTUDACTIVEMAP;
            //  _db.Connection.Execute(spName, sendToDB.ToArray(), commandType: CommandType.StoredProcedure))
            var rowsUpdated = _db.Connection.Execute(spName, new { SectionId = batchId },
                commandType: CommandType.StoredProcedure);
            return await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName1, delRecIds, commandType: CommandType.StoredProcedure));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<StudentDropdownModel>> GetMappedStudentByName(string StudentName, int SectionId)
        {
            var spName = SPNames.SP_GETMAPPEDSTUDENTBYNAME; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<StudentDropdownModel>(spName,
                new
                {
                    StudentName = StudentName,
                    SectionId = SectionId
                }, commandType: CommandType.StoredProcedure).ToList());
        }

    }
}
