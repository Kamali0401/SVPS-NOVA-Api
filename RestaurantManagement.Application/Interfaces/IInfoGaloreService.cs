using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IInfoGaloreService
    {
       
        
        Task<InfoGaloreDto> InsertInfoGalore(InfoGaloreDto model);
        Task UpdateInfoGalore(int id, string target);
        Task<IEnumerable<InfoGaloreDto>> GetAllInfoGalore(string infoType, int? id);
        Task<List<AttachmentModelDto>> GetAttachmentAsync(int id, string type);

    }
}
