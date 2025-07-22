using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Inventorys.
    /// </summary>
    public interface IHouseActivityService
    {/// <summary>
     /// Retrieves Inventorys optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the houseActivityDto to retrieve. If not provided, retrieves all Inventorys.</param>
     /// <returns>
     /// The task result contains a collection of houseActivityDto DTOs. if successful, or null if no Inventorys match the provided identifier.
     /// </returns>
        Task<IEnumerable<HouseActivityDto>> GetHouseActivity(int? id);
        /// <summary>
        /// Inserts a new houseActivityDto.
        /// </summary>
        /// <param name="houseActivityDto">The DTO representing the houseActivityDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<HouseActivityDto> InsertHouseActivity(HouseActivityDto houseActivityDto);

        /// <summary>
        /// Updates an existing houseActivityDto.
        /// </summary>
        /// <param name="houseActivityDto">The DTO representing the updated houseActivityDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateHouseActivity(HouseActivityDto houseActivityDto);
        /// <summary>
        /// Deletes a houseActivityDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the houseActivityDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteHouseActivityDetails(int id);
        Task<IEnumerable<HousePointModelDto>> GetHousePointDetails();
    }
}