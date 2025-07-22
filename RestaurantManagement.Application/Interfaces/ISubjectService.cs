using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Subject.
    /// </summary>
    public interface ISubjectService
    {/// <summary>
     /// Retrieves Subject optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the Subject to retrieve. If not provided, retrieves all Subject.</param>
     /// <returns>
     /// The task result contains a collection of Subject DTOs. if successful, or null if no Subject match the provided identifier.
     /// </returns>
        Task<IEnumerable<SubjectDto>> GetSubjectDetails(int? id);
        /// <summary>
        /// Inserts a new Subject.
        /// </summary>
        /// <param name="subject">The DTO representing the Subject to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<SubjectDto> InsertSubjectDetails(SubjectDto subject);

        /// <summary>
        /// Updates an existing Subject.
        /// </summary>
        /// <param name="subject">The DTO representing the updated Subject.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateSubjectDetails(SubjectDto subject);
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