using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IAttendanceService
    {

        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the AttendanceDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of AttendanceDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<AttendanceDto>> GetAttendanceDetails(DateTime? AttendanceDate, int sectionId, string Hoursday);
        Task<IEnumerable<AttendanceDto>> GetAttendanceById(int? id);
        Task<IEnumerable<StudentAttendanceModelDto>> GetAttendanceByStudentId(int studentId, int month, int year);

        /// <summary>
        /// Inserts a new AttendanceDto.
        /// </summary>
        /// <param name="AttendanceDto">The DTO representing the AttendanceDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<string> InsertAttendanceDetails(List<AttendanceDto> AttendanceDto);

        /// <summary>
        /// Updates an existing AttendanceDto.
        /// </summary>
        /// <param name="AttendanceDto">The DTO representing the updated AttendanceDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task<string> DeleteAttendanceDetails(List<AttendanceDto> AttendanceDto);

        Task<List<AttendanceDto>> UpdateAttendanceDetails(AttendanceDto AttendanceDto);
        /// <summary>
        /// Deletes a AttendanceDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AttendanceDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        //  Task<int> DeleteAttendanceDetails(int[] ids, int batchId);
        //   Task<IEnumerable<StudentDropdownModelDto>> GetMappedStudentByName(string StudentName, int SectionId);

    }
}
