using RestaurantManagement.Domain.Entities;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Inventorys.
    /// </summary>
    public interface IUpcomingCompetitionRepository
    { /// <summary>
      /// Retrieves Inventorys optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Inventorys to retrieve. If not provided, retrieves all Inventoryss.</param>
      /// <returns>
      /// The task result contains a collection of Inventorys if successful, or null if no Inventoryss match the provided identifier.
      /// </returns>
        Task<IEnumerable<UpcomingCompetition>> GetUpcomingCompetition(string role, int? id);
        Task<IEnumerable<UpcomingCompetition>> GetUpcomingCompetitionbyId(int? id);
        /// <summary>
        /// Inserts a new Inventorys.
        /// </summary>
        /// <param name="inventorys">The Inventorys to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<IEnumerable<UpcomingCompetition>> UpdateInterestedCompetition(int studentId, int competitionId);
        Task<UpcomingCompetition> InsertUpcomingCompetition(UpcomingCompetition inventorys);
        /// <summary>
        /// Updates an existing Inventorys.
        /// </summary>
        /// <param name="inventorys">The Inventorys to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateUpcomingCompetition(UpcomingCompetition inventorys);
        /// <summary>
        /// Deletes a Inventorys by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Inventorys to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteUpcomingCompetitionDetails(int id);
        Task<string> GetInterestedStudentList(int competitionId);

    }
}
