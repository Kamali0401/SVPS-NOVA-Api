using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IUploadAttachmentsService
    {
        Task<String> UpdateActivityFilepathdata(string target, int id, string files);
        Task<String> UpdateFilepathdata(string target, int id, string files,string TypeofUser);
    }
}
