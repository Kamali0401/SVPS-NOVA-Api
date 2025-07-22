using RestaurantManagement.Domain.Entities;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on AcademicCalendars.
    /// </summary>
    public interface IAcademicCalendarRepository
    { /// <summary>
      /// Retrieves AcademicCalendars optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the AcademicCalendars to retrieve. If not provided, retrieves all AcademicCalendarss.</param>
      /// <returns>
      /// The task result contains a collection of AcademicCalendars if successful, or null if no AcademicCalendars match the provided identifier.
      /// </returns>
        Task<IEnumerable<AcademicCalendar>> GetAcademicCalendar(int? id);
        Task<IEnumerable<AcademicCalendar>> GetAllAcademicCalendar(string role);
        /// <summary>
        /// Inserts a new AcademicCalendar.
        /// </summary>
        /// <param name="roles">The AcademicCalendar to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<AcademicCalendar> InsertAcademicCalendarDetails(AcademicCalendar roles);
        /// <summary>
        /// Updates an existing AcademicCalendar.
        /// </summary>
        /// <param name="roles">The AcademicCalendar to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateAcademicCalendarDetails(AcademicCalendar roles);
        /// <summary>
        /// Deletes a AcademicCalendar by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AcademicCalendar to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAcademicCalendarDetails(int id);
    }
}
