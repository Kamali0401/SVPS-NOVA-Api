using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on TimeTable.
    /// </summary>
    public interface ITimeTableService
    {/// <summary>
     /// Retrieves TimeTable optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the TimeTableDto to retrieve. If not provided, retrieves all TimeTable.</param>
     /// <returns>
     /// The task result contains a collection of TimeTableDto DTOs. if successful, or null if no TimeTable match the provided identifier.
     /// </returns>
        Task<IEnumerable<TimeTableDto>> GetTimeTableDetails(int? id);
        /// <summary>
        /// Inserts a new TimeTableDto.
        /// </summary>
        /// <param name="TimeTableDto">The DTO representing the TimeTableDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<TimeTableDto> InsertTimeTableDetails(TimeTableDto TimeTableDto);

        /// <summary>
        /// Updates an existing TimeTableDto.
        /// </summary>
        /// <param name="TimeTableDto">The DTO representing the updated TimeTableDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateTimeTableDetails(TimeTableDto TimeTableDto);
        /// <summary>
        /// Deletes a TimeTableDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the TimeTableDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteTimeTableDetails(int id);
        Task<IEnumerable<TimeTableDto>> GetTimeTableBySectionId(int sectionId, string role);
    }
}