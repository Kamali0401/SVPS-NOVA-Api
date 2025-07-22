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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public AnnouncementRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Announcement>> GetAnnouncement(int? id)
        {
            var spName = SPNames.SP_GETANNOUNCEMENTBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Announcement>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Announcement>> GetAllAnnouncement(int? id, bool isReadToSendData)
        {
            var spName = SPNames.SP_GETALLANNOUNCEMENTDETAILS; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Announcement>(spName,
                new {
                    Id = id,
                    IsReadToSendData = isReadToSendData
                }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Announcement> InsertAnnouncementDetails(Announcement announcement)
        {
            var spName = SPNames.SP_INSERTANNOUNCEMENTDETAILS; // Your stored procedure name

            var parameters = new
            {
                Id = announcement.Id,
                AnnouncementDate = announcement.AnnouncementDate,
                SenderType = announcement.SenderType,
                EnglishTranslate = announcement.EnglishTranslate,
                TamilTranslate = announcement.TamilTranslate,
                IsReadytoSend = announcement.IsReadytoSend,
                IsEmailSend = announcement.IsEmailSend,
                CreatedBy = announcement.CreatedBy,
                CreatedDate = announcement.CreatedDate
            };

            // If your SP returns the inserted row or just the Id, change accordingly
            /*var result = await _db.Connection.QuerySingleOrDefaultAsync<Announcement>(
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
                announcement.Id = newStudentId.Value;
            }

            return announcement; // if the SP returns null, return the input object
        }
        /// <inheritdoc/>


        public async Task<bool> DeleteAnnouncementDetails(int id)
        {
            var spName = SPNames.SP_DELETEANNOUNCEMENTDETAILS; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }

    }
}
