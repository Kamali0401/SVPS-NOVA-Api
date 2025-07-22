using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public  interface IInfoGaloreRepository
    {
        Task<InfoGalore> InsertInfoGaloreDetails(InfoGalore model);
        Task UpdateInfoGaloreDetails(int id, string target);
        Task<IEnumerable<InfoGalore>> GetAllInfoGalore(string infoType, int? id);
    }
}
