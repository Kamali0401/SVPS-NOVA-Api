using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{

    /// <summary>
    /// Repository interface for performing CRUD operations on Section.
    /// </summary>
    public interface ISectionRepository
    {
        /// <summary>
        /// Retrieves Section optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the Section to retrieve. If not provided, retrieves all Sections.</param>
        /// <returns>
        /// The task result contains a collection of Section if successful, or null if no Sections match the provided identifier.
        /// </returns>
        Task<IEnumerable<SectionMaster>> GetSectionDetails(int? id);
        /// <summary>
        /// Inserts a new Section.
        /// </summary>
        /// <param name="Section">The Section to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<SectionMaster> InsertSectionDetails(SectionMaster Section);
        /// <summary>
        /// Updates an existing Section.
        /// </summary>
        /// <param name="Section">The Section to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateSectionDetails(SectionMaster Section);
        /// <summary>
        /// Deletes a Section by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Section to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteSectionDetails(int id);
    }
}
