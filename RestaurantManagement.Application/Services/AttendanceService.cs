using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{ /// <summary>
  /// Service class for performing CRUD operations on Attendances.
  /// </summary>
    public class AttendanceService : IAttendanceService
    {


        private readonly IAttendanceRepository _AttendanceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceService"/> class.
        /// </summary>
        /// <param name="AttendanceRepository">The repository for accessing AttendanceDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public AttendanceService(IAttendanceRepository AttendanceRepository, IMapper mapper)
        {
            _AttendanceRepository = AttendanceRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<AttendanceDto>> GetAttendanceDetails(DateTime? AttendanceDate, int sectionId, string Hoursday)
        {
            var Attendances = await _AttendanceRepository.GetAttendanceDetails(AttendanceDate, sectionId, Hoursday);

            var AttendanceDetails = _mapper.Map<IEnumerable<AttendanceDto>>(Attendances);
            return AttendanceDetails;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AttendanceDto>> GetAttendanceById(int? id)
        {
            var Attendances = await _AttendanceRepository.GetAttendanceById(id);

            var AttendanceDetails = _mapper.Map<IEnumerable<AttendanceDto>>(Attendances);
            return AttendanceDetails;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<StudentAttendanceModelDto>> GetAttendanceByStudentId(int studentId, int month, int year)
        {
            var Attendances = await _AttendanceRepository.GetAttendanceByStudentId(studentId, month, year);

            var AttendanceDetails = _mapper.Map<IEnumerable<StudentAttendanceModelDto>>(Attendances);
            return AttendanceDetails;
        }

        /// <inheritdoc/>
        public async Task<string> InsertAttendanceDetails(List<AttendanceDto> attendanceDtos)
        {
            var attendanceEntities = _mapper.Map<List<Attendance>>(attendanceDtos);
            var insertedData = await _AttendanceRepository.InsertAttendanceDetails(attendanceEntities);

            if (string.IsNullOrEmpty(insertedData))
            {
                throw new Exception("Attendance insertion failed.");
            }

            return insertedData;
        }

        /// <inheritdoc/>
        public async Task<List<AttendanceDto>> UpdateAttendanceDetails(AttendanceDto attendanceDto)
        {
            var attendanceEntity = _mapper.Map<Attendance>(attendanceDto);
            var updatedEntities = await _AttendanceRepository.UpdateAttendanceDetails(attendanceEntity);

            return _mapper.Map<List<AttendanceDto>>(updatedEntities);
        }

        /// <inheritdoc/>
        public async Task<string> DeleteAttendanceDetails(List<AttendanceDto> attendanceDtos)
        {
            var attendanceEntities = _mapper.Map<List<Attendance>>(attendanceDtos);
            return await _AttendanceRepository.DeleteAttendanceDetails(attendanceEntities);
        }


    }
}
