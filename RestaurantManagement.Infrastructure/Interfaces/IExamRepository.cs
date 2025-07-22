using RestaurantManagement.Domain.Entities;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Exams.
    /// </summary>
    public interface IExamRepository
    { /// <summary>
      /// Retrieves Exams optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Exams to retrieve. If not provided, retrieves all Examss.</param>
      /// <returns>
      /// The task result contains a collection of Exams if successful, or null if no Exams match the provided identifier.
      /// </returns>
        Task<IEnumerable<Exam>> GetExam(int? id);
        /// <summary>
        /// Inserts a new Exam.
        /// </summary>
        /// <param name="roles">The Exam to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Exam> InsertExamDetails(Exam roles);
        /// <summary>
        /// Updates an existing Exam.
        /// </summary>
        /// <param name="roles">The Exam to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateExamDetails(Exam roles);
        /// <summary>
        /// Deletes a Exam by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Exam to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteExamDetails(int id);


    }
}
