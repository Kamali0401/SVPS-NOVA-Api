using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Houses.
    /// </summary>
    public interface IHouseRepository
    { /// <summary>
      /// Retrieves Houses optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Houses to retrieve. If not provided, retrieves all Housess.</param>
      /// <returns>
      /// The task result contains a collection of Houses if successful, or null if no Houses match the provided identifier.
      /// </returns>
        Task<IEnumerable<House>> GetHouse(int? id);
        /// <summary>
        /// Inserts a new House.
        /// </summary>
        /// <param name="roles">The House to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<House> InsertHouseDetails(House roles);
        /// <summary>
        /// Updates an existing House.
        /// </summary>
        /// <param name="roles">The House to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateHouseDetails(House roles);
        /// <summary>
        /// Deletes a House by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the House to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteHouseDetails(int id);
       
        
    }
}
