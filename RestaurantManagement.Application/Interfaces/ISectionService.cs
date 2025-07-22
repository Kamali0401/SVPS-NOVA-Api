using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on SectionDto.
    /// </summary>
    public interface ISectionService
    {
        /// <summary>
        /// Retrieves SectionDto optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the SectionDto to retrieve. If not provided, retrieves all SectionDto.</param>
        /// <returns>
        /// The task result contains a collection of SectionDto DTOs. if successful, or null if no SectionDto match the provided identifier.
        /// </returns>
        Task<IEnumerable<SectionDto>> GetSectionDetails(int? id);
        /// <summary>
        /// Inserts a new SectionDto.
        /// </summary>
        /// <param name="SectionDto">The DTO representing the SectionDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<SectionDto> InsertSectionDetails(SectionDto SectionDto);

        /// <summary>
        /// Updates an existing SectionDto.
        /// </summary>
        /// <param name="SectionDto">The DTO representing the updated SectionDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateSectionDetails(SectionDto SectionDto);
        /// <summary>
        /// Deletes a SectionDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SectionDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteSectionDetails(int id);
    }
}
