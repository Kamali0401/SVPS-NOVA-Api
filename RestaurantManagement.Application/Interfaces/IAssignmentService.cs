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
    public interface IAssignmentService
    {/// <summary>
     /// Retrieves Inventorys optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the AssignmentDto to retrieve. If not provided, retrieves all Inventorys.</param>
     /// <returns>
     /// The task result contains a collection of AssignmentDto DTOs. if successful, or null if no Inventorys match the provided identifier.
     /// </returns>
        Task<IEnumerable<AssignmentDto>> GetAssignment(int? id);
        /// <summary>
        /// Inserts a new AssignmentDto.
        /// </summary>
        /// <param name="AssignmentDto">The DTO representing the AssignmentDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<AssignmentDto> InsertAssignmentDetails(AssignmentDto AssignmentDto);

        /// <summary>
        /// Updates an existing AssignmentDto.
        /// </summary>
        /// <param name="AssignmentDto">The DTO representing the updated AssignmentDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateAssignmentDetails(AssignmentDto AssignmentDto);
        /// <summary>
        /// Deletes a AssignmentDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AssignmentDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAssignmentDetails(int id);
        Task<IEnumerable<AssignmentDto>> GetAllAssignmentByStudent(string role, int studentId);
    }
}