using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on TimeTables.
    /// </summary>
    public interface ITimeTableRepository
    { /// <summary>
      /// Retrieves TimeTables optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the TimeTables to retrieve. If not provided, retrieves all TimeTabless.</param>
      /// <returns>
      /// The task result contains a collection of TimeTables if successful, or null if no TimeTabless match the provided identifier.
      /// </returns>
        Task<IEnumerable<TimeTable>> GetTimeTableDetails(int? id);
        /// <summary>
        /// Inserts a new TimeTables.
        /// </summary>
        /// <param name="TimeTable">The TimeTables to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<TimeTable> InsertTimeTableDetails(TimeTable TimeTable);
        /// <summary>
        /// Updates an existing TimeTables.
        /// </summary>
        /// <param name="TimeTable">The TimeTables to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateTimeTableDetails(TimeTable TimeTable);
        /// <summary>
        /// Deletes a TimeTables by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the TimeTables to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteTimeTableDetails(int id);
        Task<IEnumerable<TimeTable>> GetTimeTableBySectionId(int sectionId, string role);

    }
}
