using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Roles.
    /// </summary>
    public interface IRoleService
    {/// <summary>
     /// Retrieves Roles optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the RoleDto to retrieve. If not provided, retrieves all Roles.</param>
     /// <returns>
     /// The task result contains a collection of RoleDto DTOs. if successful, or null if no Roles match the provided identifier.
     /// </returns>
        Task<IEnumerable<RoleDto>> GetRolesDetails(int? id);
        /// <summary>
        /// Inserts a new RoleDto.
        /// </summary>
        /// <param name="roleDto">The DTO representing the RoleDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<RoleDto> InsertRoleDetails(RoleDto roleDto);

        /// <summary>
        /// Updates an existing RoleDto.
        /// </summary>
        /// <param name="roleDto">The DTO representing the updated RoleDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateRoleDetails(RoleDto roleDto);
        /// <summary>
        /// Deletes a RoleDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the RoleDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteRoleDetails(int id);
    }
}