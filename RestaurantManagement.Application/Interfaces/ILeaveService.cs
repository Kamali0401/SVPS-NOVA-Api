using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Inventorys.
    /// </summary>
    public interface ILeaveService
    {/// <summary>
     /// Retrieves Inventorys optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the LeaveDto to retrieve. If not provided, retrieves all Inventorys.</param>
     /// <returns>
     /// The task result contains a collection of LeaveDto DTOs. if successful, or null if no Inventorys match the provided identifier.
     /// </returns>
        Task<IEnumerable<LeaveDto>> GetLeave(string role,int? id);
        Task<IEnumerable<LeaveDto>> GetLeaveById(int? id);
        /// <summary>
        /// Inserts a new LeaveDto.
        /// </summary>
        /// <param name="LeaveDto">The DTO representing the LeaveDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<LeaveDto> InsertLeaveDetails(LeaveDto LeaveDto);

        /// <summary>
        /// Updates an existing LeaveDto.
        /// </summary>
        /// <param name="LeaveDto">The DTO representing the updated LeaveDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateLeaveDetails(LeaveDto LeaveDto);
        /// <summary>
        /// Deletes a LeaveDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the LeaveDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteLeaveDetails(int id);
       
    }
}
