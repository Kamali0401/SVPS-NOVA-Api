using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public interface IStudentFeedbackService
    {
        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the StudentFeedbackDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of StudentFeedbackDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<StudentFeedbackDto>> GetStudentFeedback(int? id);
        Task<IEnumerable<StudentFeedbackDto>> GetAllStudentFeedback(string role, int? id);
        /// <summary>
        /// Inserts a new StudentFeedbackDto.
        /// </summary>
        /// <param name="StudentFeedbackDto">The DTO representing the StudentFeedbackDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<string> InsertStudentFeedbackDetails(List<StudentFeedbackDto> StudentFeedbackDto);

        /// <summary>
        /// Updates an existing StudentFeedbackDto.
        /// </summary>
        /// <param name="StudentFeedbackDto">The DTO representing the updated StudentFeedbackDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
      //  Task UpdateStudentFeedbackDetails(StudentFeedbackDto StudentFeedbackDto);
        /// <summary>
        /// Deletes a StudentFeedbackDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the StudentFeedbackDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<List<StudentFeedbackDto>> DeleteStudentFeedbackDetails(string id);
    }
}
