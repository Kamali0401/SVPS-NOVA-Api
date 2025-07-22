using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Roles.
    /// </summary>
    public interface IHouseService
    {/// <summary>
     /// Retrieves Roles optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the HouseDto to retrieve. If not provided, retrieves all Roles.</param>
     /// <returns>
     /// The task result contains a collection of HouseDto DTOs. if successful, or null if no Roles match the provided identifier.
     /// </returns>
        Task<IEnumerable<HouseDto>> GetHouse(int? id);
        /// <summary>
        /// Inserts a new HouseDto.
        /// </summary>
        /// <param name="houseDto">The DTO representing the HouseDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<HouseDto> InsertHouseDetails(HouseDto houseDto);

        /// <summary>
        /// Updates an existing HouseDto.
        /// </summary>
        /// <param name="houseDto">The DTO representing the updated HouseDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateHouseDetails(HouseDto houseDto);
        /// <summary>
        /// Deletes a HouseDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the HouseDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteHouseDetails(int id);
    }
}