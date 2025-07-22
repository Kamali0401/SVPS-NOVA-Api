using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Activitys.
    /// </summary>
    public interface IActivityService
    {/// <summary>
     /// Retrieves Activitys optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the ActivityDto to retrieve. If not provided, retrieves all Activitys.</param>
     /// <returns>
     /// The task result contains a collection of ActivityDto DTOs. if successful, or null if no Activitys match the provided identifier.
     /// </returns>
        Task<IEnumerable<ActivityDto>> GetActivityData(int? id);
        /// <summary>
        /// Inserts a new ActivityDto.
        /// </summary>
        /// <param name="ActivityDto">The DTO representing the ActivityDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<ActivityDto> InsertActivityData(ActivityDto ActivityDto);

        /// <summary>
        /// Updates an existing ActivityDto.
        /// </summary>
        /// <param name="ActivityDto">The DTO representing the updated ActivityDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateActivityData(ActivityDto ActivityDto);
        /// <summary>
        /// Deletes a ActivityDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ActivityDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteActivityData(int id);
        Task<IEnumerable<ActivityDto>> GetAllActivityData(int Type, long? DepartmentId);

    }
}