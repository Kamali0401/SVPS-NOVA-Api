using Dapper;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Repositories
{ /// <summary>
  /// Repository class for performing CRUD operations on bill.
  /// </summary>
    public class UploadAttachmentsRepository : IUploadAttachmentsRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingCompetitionRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public UploadAttachmentsRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        public Task<string> UpdateActivityFilepathdata(string target, int id, string filesList)
        {
            var spName = SPNames.SP_UPDATEACTIVITYFILE;
            //  var list = new List<string>;

            return Task.Factory.StartNew(() => _db.Connection.Query<string>(spName,
                    new { Id = id, filepath = target, files = filesList }, commandType: CommandType.StoredProcedure)
                .ToString());
        }

        public Task<string> UpdateFilepathdata(string target, int id, string filesList, string TypeofFile)
        {
            var spName = SPNames.SP_UPDATEFILE;
            //  var list = new List<string>;

            return Task.Factory.StartNew(() => _db.Connection.Query<string>(spName,
                new { Id = id, filepath = target, files = filesList, typeofFile = TypeofFile },
                commandType: CommandType.StoredProcedure).ToString());
        }
    }
}
