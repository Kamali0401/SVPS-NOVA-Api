using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on students.
    /// </summary>
    public interface IStudentService
    {/// <summary>
     /// Retrieves students optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the studentDto to retrieve. If not provided, retrieves all students.</param>
     /// <returns>
     /// The task result contains a collection of studentDto DTOs. if successful, or null if no students match the provided identifier.
     /// </returns>
        Task<IEnumerable<StudentDto>> GetStudentDetails(int? id);
        /// <summary>
        /// Inserts a new studentDto.
        /// </summary>
        /// <param name="studentDto">The DTO representing the studentDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<StudentDto> InsertStudent(StudentDto studentDto);

        /// <summary>
        /// Updates an existing studentDto.
        /// </summary>
        /// <param name="studentDto">The DTO representing the updated studentDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateStudent(StudentDto studentDto);
        /// <summary>
        /// Deletes a studentDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the studentDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteStudent(int id);
        Task<IEnumerable<StudentDropdownDto>> GetStudentByName(string studentname);
        Task updateStudentSemDateDetails(StudentSemDateModelDto studentDto);
        Task<List<StudentSemDateModelDto>> GetAllStudentConfiguration(int? id);
    }
}