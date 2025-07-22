using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public  interface IAttendanceReportRepository
    {
        public Task<string> generateMonthlyAttendancereport(int startMonth, int startYear, int endMonth, int endYear, int sectionId, string grade, string section);

        public Task<string> generateDailyAttendancereport(int month, int year, int sectionId, string grade, string section);
        public Task<string> generateAttendanceCumulativereport(int startYear, int endYear, int sectionId);
        public Task<string> generateExcelList(string role);
       // Task<(MemoryStream memory, string path)> DownloadData(string filepath);
    }
}
