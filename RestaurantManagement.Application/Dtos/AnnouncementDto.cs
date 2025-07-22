using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public  class AnnouncementDto
    {
        public long Id { get; set; }
        public DateTime AnnouncementDate { get; set; }= DateTime.UtcNow;
        //public string Department { get; set; }
        //public string Year { get; set; }
        //public string Semester { get; set; }
        public string? Filepath { get; set; }
        public string? FileNames { get; set; }
        public List<string>? Files { get; set; }
        public string SenderType { get; set; } = string.Empty;


        public bool IsEmailSend { get; set; }=false;
        public string EnglishTranslate { get; set; } = string.Empty;
        public string TamilTranslate { get; set; } = string.Empty;
        public bool IsReadytoSend { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
