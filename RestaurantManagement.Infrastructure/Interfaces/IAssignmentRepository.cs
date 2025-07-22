using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Assignments.
    /// </summary>
    public interface IAssignmentRepository
    { /// <summary>
      /// Retrieves Assignments optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Assignments to retrieve. If not provided, retrieves all Assignmentss.</param>
      /// <returns>
      /// The task result contains a collection of Assignments if successful, or null if no Assignments match the provided identifier.
      /// </returns>
        Task<IEnumerable<Assignment>> GetAssignment(int? id);
        /// <summary>
        /// Inserts a new Assignment.
        /// </summary>
        /// <param name="Assignment">The Assignment to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Assignment> InsertAssignmentDetails(Assignment Assignment);
        /// <summary>
        /// Updates an existing Assignment.
        /// </summary>
        /// <param name="Assignment">The Assignment to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateAssignmentDetails(Assignment Assignment);
        /// <summary>
        /// Deletes a Assignment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Assignment to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAssignmentDetails(int id);
        Task<IEnumerable<Assignment>> GetAllAssignmentByStudent(string role, int studentId);


    }
}
