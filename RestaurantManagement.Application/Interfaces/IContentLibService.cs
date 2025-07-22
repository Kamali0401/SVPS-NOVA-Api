using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on ContentLibs.
    /// </summary>
    public interface IContentLibService
    {/// <summary>
     /// Retrieves ContentLibs optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the ContentLibingDto to retrieve. If not provided, retrieves all ContentLibs.</param>
     /// <returns>
     /// The task result contains a collection of ContentLibingDto DTOs. if successful, or null if no ContentLibs match the provided identifier.
     /// </returns>
        Task<IEnumerable<ContentLibDto>> GetContentLibDetails(int? id);
        /// <summary>
        /// Inserts a new ContentLibingDto.
        /// </summary>
        /// <param name="ContentLibingDto">The DTO representing the ContentLibingDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<ContentLibDto> InsertContentLibDetails(ContentLibDto ContentLibDto);

        /// <summary>
        /// Updates an existing ContentLibingDto.
        /// </summary>
        /// <param name="ContentLibingDto">The DTO representing the updated ContentLibingDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateContentLibDetails(ContentLibDto ContentLibDto);
        /// <summary>
        /// Deletes a ContentLibingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ContentLibingDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteContentLibDetails(int id);
        Task<IEnumerable<ContentLibDto>> GetAllContentLibByStudent(int student);
    }
}