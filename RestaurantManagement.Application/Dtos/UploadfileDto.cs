using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public class UploadfileDto

    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public List<IFormFile> FormFiles { get; set; }
    }


    public class FileUploadDto
    {
        public int Id { get; set; }
        public string TypeofUser { get; set; }

        public List<IFormFile> FormFiles { get; set; }
    }
}
