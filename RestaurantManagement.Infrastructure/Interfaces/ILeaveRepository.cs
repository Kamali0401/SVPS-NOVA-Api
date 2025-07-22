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
    /// Repository interface for performing CRUD operations on Leaves.
    /// </summary>
    public interface ILeaveRepository
    {
        /// <summary>
        /// Retrieves Leaves optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the Leaves to retrieve. If not provided, retrieves all Leavess.</param>
        /// <returns>
        /// The task result contains a collection of Leaves if successful, or null if no Leaves match the provided identifier.
        /// </returns>
        Task<IEnumerable<Leave>> GetLeave(string role, int? id);
        Task<IEnumerable<Leave>> GetLeaveById(int? id);
        /// <summary>
        /// Inserts a new Leave.
        /// </summary>
        /// <param name="Leave">The Leave to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Leave> InsertLeaveDetails(Leave Leave);
        /// <summary>
        /// Updates an existing Leave.
        /// </summary>
        /// <param name="Leave">The Leave to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateLeaveDetails(Leave Leave);
        /// <summary>
        /// Deletes a Leave by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Leave to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteLeaveDetails(int id);
        
    }
}
