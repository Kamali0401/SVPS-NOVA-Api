using AutoMapper;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using SonaNova.Application.Interfaces;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{
    public  class AttendancereportService :IAttendanceReport
    {
        /// Service class for performing CRUD operations on AttendanceReports.
        /// </summary>
        
            private readonly IAttendanceReportRepository _AttendanceReportRepository;
            private readonly IMapper _mapper;

            /// <summary>
            /// Initializes a new instance of the <see cref="AttendanceReportService"/> class.
            /// </summary>
            /// <param name="AttendanceReportRepository">The repository for accessing AttendanceReportDto data.</param>
            /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
            public AttendancereportService(IAttendanceReportRepository AttendanceReportRepository, IMapper mapper)
            {
                _AttendanceReportRepository = AttendanceReportRepository;
                _mapper = mapper;
            }


        public async Task<(MemoryStream memory, string path)> DownloadData(string filepath)
        {
            var Path = filepath;
            var memorys = new MemoryStream();
            using (var stream = new FileStream(Path, FileMode.Open))
            {
                await stream.CopyToAsync(memorys);
            }
            return (memory: memorys, path: Path);
        }
        public virtual async Task<string> generateMonthlyAttendancereport(int startMonth, int startYear, int endMonth, int endYear, int sectionId, string grade, string section)
        {
            return await _AttendanceReportRepository.generateMonthlyAttendancereport(startMonth, startYear, endMonth, endYear, sectionId, grade, section);

        }
        public virtual async Task<string> generateExcelList(string role)
        {
            return await _AttendanceReportRepository.generateExcelList(role);

        }




        public virtual async Task<string> generateDailyAttendancereport(int month, int year, int sectionId, string grade, string section)
        {
            return await _AttendanceReportRepository.generateDailyAttendancereport(month, year, sectionId, grade, section);

        }

        public virtual async Task<string> generateAttendanceCumulativereport(int startYear, int endYear, int sectionId)
        {
            return await _AttendanceReportRepository.generateAttendanceCumulativereport(startYear, endYear, sectionId);

        }
    }
}
