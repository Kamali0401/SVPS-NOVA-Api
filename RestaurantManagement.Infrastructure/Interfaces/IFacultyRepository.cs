using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Interfaces
{/// <summary>
 /// Repository interface for performing CRUD operations on TableDetails.
 /// </summary>
    public interface IFacultyRepository
    {

        /// <summary>
        /// Retrieves TableDetails optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the TableDetails to retrieve. If not provided, retrieves all TableDetails.</param>
        /// <returns>
        /// The task result contains a collection of TableDetails if successful, or null if no TableDetails match the provided identifier.
        /// </returns>
        Task<IEnumerable<Faculty>> GetFacultyDetails(int? id);



        /// <summary>
        /// Retrieves MappingDetails optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the MappingDetails to retrieve. If not provided, retrieves all MappingDetails.</param>
        /// <returns>
        /// The task result contains a collection of MappingDetails if successful, or null if no MappingDetails match the provided identifier.
        /// </returns>
        Task<IEnumerable<FacultyDropdown>> GetFacultyByName(string facultyname);
        /// <summary>
        /// Inserts a new TableDetails.
        /// </summary>
        /// <param name="TableDetails">The TableDetails to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Faculty> InsertFaculty(Faculty TableDetails);
        /// <summary>
        /// Updates an existing TableDetails.
        /// </summary>
        /// <param name="TableDetails">The TableDetails to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateFaculty(Faculty TableDetails);
        /// <summary>
        /// Deletes a TableDetails by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the TableDetails to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteFaculty(int id);
    }
}
