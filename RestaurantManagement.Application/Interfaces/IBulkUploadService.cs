using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IBulkUploadService
    {
        Task<string> bulkuploadstudent(DataTable target);
        Task<string> bulkuploadmark(string target, string section);
        Task<string> bulkuploadfaculty(DataTable target);
        Task<string> bulkuploadsubject(DataTable target);
        Task<string> bulkuploadholidaycalendar(DataTable target);

        Task<string> bulkuploadacademiccalendar(DataTable target);

        Task<string> bulkuploadtimetable(DataTable target);
    }
}
