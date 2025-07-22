using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Subjects.
    /// </summary>
    public interface ISubjectRepository
    { /// <summary>
      /// Retrieves Subject optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Subject to retrieve. If not provided, retrieves all Subjects.</param>
      /// <returns>
      /// The task result contains a collection of Subject if successful, or null if no Subjects match the provided identifier.
      /// </returns>
        Task<IEnumerable<Subject>> GetSubjectDetails(int? id);
        /// <summary>
        /// Inserts a new Subject.
        /// </summary>
        /// <param name="Subject">The Subject to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Subject> InsertSubjectDetails(Subject Subject);
        /// <summary>
        /// Updates an existing Subject.
        /// </summary>
        /// <param name="Subject">The Subject to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateSubjectDetails(Subject Subject);
        /// <summary>
        /// Deletes a Subject by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Subject to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteSubjectDetails(int id);
    }
}
