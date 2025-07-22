using Dapper;
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
{/// <summary>
 /// Repository class for performing CRUD operations on bill.
 /// </summary>
    public class InfoGaloreRepository : IInfoGaloreRepository
    {


        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HouseRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public InfoGaloreRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        public  async Task<IEnumerable<InfoGalore>> GetAllInfoGalore(string infoType, int? id)
        {
            var spName = SPNames.SP_GETINFOGALORE;
            return await Task.Factory.StartNew(() => _db.Connection.Query<InfoGalore>(spName,
                new { @InfoType = infoType, @Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        public async Task<InfoGalore> InsertInfoGaloreDetails(InfoGalore infoGalore)
        {
            var spName = SPNames.SP_INSERTINFOGALORE;
            var result = await Task.Factory.StartNew(() =>
                _db.Connection.Query<InfoGalore>(spName, new
                {
                    infoGalore.InfoType,
                    infoGalore.InfoFileName,
                    infoGalore.ValidDate,
                    infoGalore.CreatedBy
                }, commandType: CommandType.StoredProcedure).FirstOrDefault());

            return result;
        }
        public async Task UpdateInfoGaloreDetails(int id, string target)
        {
            var spName = SPNames.SP_UPDATEINFOGALORE;
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new
                {
                    Id = id,
                    InfoFilePath = target
                }, commandType: CommandType.StoredProcedure));
        }


    }
}
