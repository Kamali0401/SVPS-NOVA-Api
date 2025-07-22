using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Roles.
    /// </summary>
    public interface IRoleRepository
    { /// <summary>
      /// Retrieves Roles optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Roles to retrieve. If not provided, retrieves all Roless.</param>
      /// <returns>
      /// The task result contains a collection of Roles if successful, or null if no Roless match the provided identifier.
      /// </returns>
        Task<IEnumerable<Roles>> GetRolesDetails(int? id);
        /// <summary>
        /// Inserts a new Roles.
        /// </summary>
        /// <param name="roles">The Roles to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Roles> InsertRoleDetails(Roles roles);
        /// <summary>
        /// Updates an existing Roles.
        /// </summary>
        /// <param name="roles">The Roles to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateRoleDetails(Roles roles);
        /// <summary>
        /// Deletes a Roles by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Roles to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteRoleDetails(int id);
        /// <summary>
        /// reset a labour password.
        /// </summary>
        /// <param name="rolename">The rolename of the labour to update.</param>
        /// <param name="password">The password of the labour to update.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task ResetPasswordAsync(int id, string rolename, string newPassword, string modifiedBy);
    }
}
