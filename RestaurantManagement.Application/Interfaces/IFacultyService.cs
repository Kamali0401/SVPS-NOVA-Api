using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{/// <summary>
 /// Service interface for performing CRUD operations on Faculty.
 /// </summary>
    public interface IFacultyService
    {
        /// <summary>
        /// Retrieves Faculty optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the FacultyDto to retrieve. If not provided, retrieves all Faculty.</param>
        /// <returns>
        /// The task result contains a collection of FacultyDto DTOs. if successful, or null if no Faculty match the provided identifier.
        /// </returns>
        Task<IEnumerable<FacultyDto>> GetFacultyDetails(int? id);


       
        /// <summary>
        /// Inserts a new FacultyDto.
        /// </summary>
        /// <param name="FacultyDto">The DTO representing the FacultyDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<FacultyDto> InsertFaculty(FacultyDto FacultyDto);

        /// <summary>
        /// Updates an existing FacultyDto.
        /// </summary>
        /// <param name="FacultyDto">The DTO representing the updated FacultyDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateFaculty(FacultyDto FacultyDto);
        /// <summary>
        /// Deletes a FacultyDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the FacultyDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteFaculty(int id);
        Task<IEnumerable<FacultyDropdowndto>> GetFacultyByName(string facultyname);
    }
}
