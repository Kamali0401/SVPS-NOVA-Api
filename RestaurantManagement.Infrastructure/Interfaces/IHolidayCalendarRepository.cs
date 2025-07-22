using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on HolidayCalendars.
    /// </summary>
    public interface IHolidayCalendarRepository
    { /// <summary>
      /// Retrieves HolidayCalendars optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the HolidayCalendar to retrieve. If not provided, retrieves all HolidayCalendars.</param>
      /// <returns>
      /// The task result contains a collection of HolidayCalendars if successful, or null if no HolidayCalendars match the provided identifier.
      /// </returns>
        Task<IEnumerable<HolidayCalendar>> GetHolidayCalendarDetails(int? id);
        /// <summary>
        /// Inserts a new HolidayCalendar.
        /// </summary>
        /// <param name="HolidayCalendar">The HolidayCalendar to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<HolidayCalendar> InsertHolidayCalendarDetails(HolidayCalendar HolidayCalendar);
        /// <summary>
        /// Updates an existing HolidayCalendar.
        /// </summary>
        /// <param name="HolidayCalendar">The HolidayCalendar to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateHolidayCalendarDetails(HolidayCalendar HolidayCalendar);
        /// <summary>
        /// Deletes a HolidayCalendar by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the HolidayCalendar to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteHolidayCalendarDetails(int id);
    }
}
