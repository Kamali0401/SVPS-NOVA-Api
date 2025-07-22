using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on HolidayCalendars.
    /// </summary>
    public interface IHolidayCalendarService
    {/// <summary>
     /// Retrieves HolidayCalendars optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the HolidayCalendarDto to retrieve. If not provided, retrieves all HolidayCalendars.</param>
     /// <returns>
     /// The task result contains a collection of HolidayCalendarDto DTOs. if successful, or null if no HolidayCalendars match the provided identifier.
     /// </returns>
        Task<IEnumerable<HolidayCalendarDto>> GetHolidayCalendarDetails(int? id); 
        /// <summary>
        /// Inserts a new HolidayCalendarDto.
        /// </summary>
        /// <param name="HolidayCalendarDto">The DTO representing the HolidayCalendarDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<HolidayCalendarDto> InsertHolidayCalendarDetails(HolidayCalendarDto HolidayCalendarDto);

        /// <summary>
        /// Updates an existing HolidayCalendarDto.
        /// </summary>
        /// <param name="HolidayCalendarDto">The DTO representing the updated HolidayCalendarDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateHolidayCalendarDetails(HolidayCalendarDto HolidayCalendarDto);
        /// <summary>
        /// Deletes a HolidayCalendarDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the HolidayCalendarDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteHolidayCalendarDetails(int id);
    }
}