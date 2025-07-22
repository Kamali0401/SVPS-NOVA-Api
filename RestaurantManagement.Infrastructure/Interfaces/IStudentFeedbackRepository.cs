using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on StudentFeedbacks.
    /// </summary>
    public interface IStudentFeedbackRepository
    { /// <summary>
      /// Retrieves StudentFeedbacks optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the StudentFeedbacks to retrieve. If not provided, retrieves all StudentFeedbackss.</param>
      /// <returns>
      /// The task result contains a collection of StudentFeedbacks if successful, or null if no StudentFeedbacks match the provided identifier.
      /// </returns>
        Task<IEnumerable<StudentFeedback>> GetStudentFeedback(int? id);
        Task<IEnumerable<StudentFeedback>> GetAllStudentFeedback(string role, int? id );
        /// <summary>
        /// Inserts a new StudentFeedback.
        /// </summary>
        /// <param name="roles">The StudentFeedback to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<string> InsertStudentFeedbackDetails(List<StudentFeedback> roles);
        /// <summary>
        /// Updates an existing StudentFeedback.
        /// </summary>
        /// <param name="roles">The StudentFeedback to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
       // Task UpdateStudentFeedbackDetails(StudentFeedback roles);
        /// <summary>
        /// Deletes a StudentFeedback by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the StudentFeedback to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<List<StudentFeedback>> DeleteStudentFeedbackDetails(string id);
    }
}
